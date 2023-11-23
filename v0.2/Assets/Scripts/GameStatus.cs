using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameStatus : MonoBehaviour
{
    public float startTime;
    public float winTime;
    public TextMeshProUGUI winText;
    public TextMeshProUGUI timeText;
    public GameObject capsule;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.timeSinceLevelLoad;
        winText.text = "";
        //winTime = 10;
    }

    // Update is called once per frame
    void Update()
    {
        timeText.text="Time:"+(int)Time.timeSinceLevelLoad;
        if (Time.timeSinceLevelLoad - startTime > winTime)
        {
            capsule.GetComponent<PlayerAttribute>().Win();
            //Time.timeScale = 0;
        }
    }
}
