using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CraftingSystem : MonoBehaviour
{

    public GameObject craftingScreenUI;
    public GameObject toolsScreenUI;

    public List<string> inventoryItemList = new List<string>();
    public GameObject EventSystem;
    public GameObject FirstSelect;

    //Category Buttons
    Button toolsBTN;

    //Craft Buttons
    Button craftAxeBTN;

    //Requirement Text
    TextMeshProUGUI AxeReq1, AxeReq2;

    public bool isOpen;

    //All Blueprint
    public Blueprint AxeBLP = new Blueprint("Axe", 2, "Stone", 2, "Stick", 2);


    public static CraftingSystem Instance { get; set; }



    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }


    // Start is called before the first frame update
    void Start()
    {

        isOpen = false;

        toolsBTN = craftingScreenUI.transform.Find("ToolsButton").GetComponent<Button>();
        toolsBTN.onClick.AddListener(delegate { OpenToolsCategory(); });


        //Axe
        AxeReq1 = toolsScreenUI.transform.Find("Axe").transform.Find("req1").GetComponent<TextMeshProUGUI>();
        AxeReq2 = toolsScreenUI.transform.Find("Axe").transform.Find("req2").GetComponent<TextMeshProUGUI>();

        craftAxeBTN = toolsScreenUI.transform.Find("Axe").transform.Find("Button").GetComponent<Button>();
        craftAxeBTN.onClick.AddListener(delegate { CraftAnyItem(AxeBLP); });

    }



    void OpenToolsCategory()
    {
        craftingScreenUI.SetActive(false);
        toolsScreenUI.SetActive(true);
    }




    void CraftAnyItem(Blueprint blueprintToCraft)
    {
        //add item into inventory
        InventorySystem.Instance.AddToInventory(blueprintToCraft.itemName);

        //remove resources from inventory
        if (blueprintToCraft.numOfRequirements == 1)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
        }
        else if (blueprintToCraft.numOfRequirements == 2)
        {
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req1, blueprintToCraft.Req1amount);
            InventorySystem.Instance.RemoveItem(blueprintToCraft.Req2, blueprintToCraft.Req2amount);
        }





        //refresh list
        StartCoroutine(calculate());

        //RefreshNeededItems();


    }


    public IEnumerator calculate()
    {
        yield return 0;

        InventorySystem.Instance.ReCalculateList();

        RefreshNeededItems();

    }




    // Update is called once per frame
    void Update()
    {

        


        /*if(Input.GetKeyDown(KeyCode.C) && !isOpen)
        {

            craftingScreenUI.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;

        }
        else if (Input.GetKeyDown(KeyCode.C) && isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);

            if (!InventorySystem.Instance.isOpen)
            {
                Cursor.lockState = CursorLockMode.Locked;
            }

            isOpen = false;
        }*/
    }
    public void Toggle()
    {
        if (isOpen)
        {
            craftingScreenUI.SetActive(false);
            toolsScreenUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
           
            
        }
        else
        {
            craftingScreenUI.SetActive(true);
            //EventSystem.GetComponent<EventSystem>().firstSelectedGameObject = FirstSelect;
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
         
        }
    }


    public void RefreshNeededItems()
    {
        int stone_count = 0;
        int stick_count = 0;

        inventoryItemList = InventorySystem.Instance.itemList;

        foreach(string itemName in inventoryItemList)
        {
            switch(itemName)
            {
                case "Stone":
                    stone_count += 1;
                    break;
                case "Stick":
                    stick_count += 1;
                    break;
            }
        }

        //--- Axe---//

        AxeReq1.text = "2 Stone [" + stone_count + "]";
        AxeReq2.text = "2 Stick [" + stick_count + "]";

        if(stone_count >= 2 && stick_count >= 2)
        {
            craftAxeBTN.gameObject.SetActive(true);
        }
        else
        {
            craftAxeBTN.gameObject.SetActive(false);
        }



    }






}