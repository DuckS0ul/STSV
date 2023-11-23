using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttacked : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnTriggerEnter(Collider Collider)
    {
        if (Collider.tag == "Bullet"|| Collider.tag =="Stick")
        {
            //Destroy(Collider.gameObject);
            Destroy(this.gameObject);
            Debug.Log("enermy die");
        }

    }
}
