using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class Medicine : MonoBehaviour
{
    public GameObject playerCapsule;
    public PlayerAttribute attribute;
    public float treatment;
    // Start is called before the first frame update
    void Start()
    {
        //treatment = 10f;
        GetComponent<Renderer>().material.color = Color.green;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(15, 30, 45) * Time.deltaTime);
    }

    void OnTriggerEnter(Collider Collider)
    {
        if (Collider.tag == "Player" )
        {
            //Destroy(Collider.gameObject);
            Destroy(this.gameObject);
            if (attribute.nowHP < 300)
            {
                attribute.Healing();
                attribute.nowHP += treatment;
            }
            
            Debug.Log("medicine");
        }

    }
}
