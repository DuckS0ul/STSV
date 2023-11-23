using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Food : MonoBehaviour
{
    public GameObject playerCapsule;
    public PlayerAttribute attribute;
    public float treatment;
    // Start is called before the first frame update
    void Start()
    {
        //treatment = 10f;
        GetComponent<Renderer>().material.color = Color.yellow;
    }

    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Collider)
    {
        if (Collider.tag == "Player" )
        {

            Destroy(this.gameObject);
            if (attribute.nowSP < attribute.SP)
            {
                Debug.Log("food");
                attribute.nowSP += treatment;
            }
            
            //Debug.Log("food");
        }

    }
}
