using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{
    public GameObject EvenSystem;
    private GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void ToMain()
    {
        //Debug.Log("start");
        SceneManager.LoadScene("MainScene");
        player = GameObject.FindGameObjectWithTag("Player");
        player.GetComponent<PlayerControl>().playerControl.Player.Enable();
        player.GetComponent<PlayerControl>().playerControl.UI.Disable();
        Cursor.visible = false;
        Time.timeScale = 1f;
        //Cursor.visible = false;
        //SceneManager.LoadScene("SampleSence");
    }
    public void ToStart()
    {
        //Debug.Log("start");
        SceneManager.LoadScene("Start");
       
        //SceneManager.LoadScene("SampleSence");
    }

    
}
