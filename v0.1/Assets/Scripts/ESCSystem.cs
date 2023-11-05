using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ESCSystem : MonoBehaviour
{
    public bool isOpen;
    public GameObject ESCMenuUI;
    //public GameObject PauseText;
    // Start is called before the first frame update
    void Start()
    {
        isOpen = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Toggle()
    {
        if (isOpen)
        {
            ESCMenuUI.SetActive(false);
            Cursor.lockState = CursorLockMode.Locked;
            isOpen = false;
            //PlayerControl.otherUI = false;
        }
        else
        {
            ESCMenuUI.SetActive(true);
            //PauseText.GetComponent<Text>().text = " Press ESC to continue";
            Cursor.lockState = CursorLockMode.None;
            isOpen = true;
            //PlayerControl.otherUI = true;
        }
    }

}
