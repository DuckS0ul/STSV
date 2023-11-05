using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    float buttleTime;
    // Start is called before the first frame update
    void Start()
    {
        buttleTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        if(Time.time - buttleTime > 5)
        {
            Destroy(this.gameObject);
            Debug.Log("bullet des5");
        }
    }
    void OnTriggerEnter(Collider Collider)
    {
       
            Destroy(this.gameObject);
            Debug.Log("bullet des");       

    }
}
