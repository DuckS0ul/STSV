using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject ESCMenuUI;
    public ESCSystem EscSystem;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Click()
    {

        Debug.Log("restart");

        ESCMenuUI.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
      
        //Cursor.visible = true;
        

        EscSystem.isOpen = false; ;
     

        SceneManager.LoadScene("MainScene");

    }
}
