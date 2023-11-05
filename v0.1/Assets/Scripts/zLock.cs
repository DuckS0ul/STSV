using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zLock : MonoBehaviour
{
    public Transform people;
    public Transform player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        people.transform.Rotate(-people.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y - people.transform.rotation.eulerAngles.y, -people.transform.rotation.eulerAngles.z);
    
    }
}
