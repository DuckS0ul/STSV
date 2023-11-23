using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    public string ItemName;

    public bool playerInRange;

    private GameObject player;
    private PlayerControl PlayerControl;

    public string GetItemName()
    {
        return ItemName;
    }

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        PlayerControl= player.GetComponent<PlayerControl>();
    }

    private void Update()
    {
        if (PlayerControl.pick && playerInRange && SelectionManager.Instance.onTarget && SelectionManager.Instance.selectedObject == gameObject)
        {
            if (!InventorySystem.Instance.CheckIfFull())
            {

                InventorySystem.Instance.AddToInventory(ItemName);
                Destroy(gameObject);

            }
            else
            {
                Debug.Log("inventory is full");
            }
        }
    }





    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }






    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}
