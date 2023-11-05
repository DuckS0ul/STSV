using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

  
public class EnemySimple : MonoBehaviour
{
    public Transform target;
    private float stoppingDistance;
    public NavMeshAgent nav;
    public float updateDelay = 0.3f;
    private float updateDeadline;

    public int enemyType;
    private Vector3 originalPos;
    private float alertRadius = 15.0f;
    private float wanderingRadius = 12.0f;
    

    private void LookAtTarget() {
        Vector3 lookPos = target.position - transform.position;
        lookPos.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 0.2f);
    }

    private void TowardsTarget() {
        if ( Time.time >= updateDeadline ){
            updateDeadline = Time.time + updateDelay;
            nav.SetDestination(target.position);
        }
    }

    private void Wandering() {
        //Debug.Log("Pretend I am wandering.....");
        Vector3 randomDirection = Random.insideUnitSphere * wanderingRadius;
        nav.SetDestination(originalPos + randomDirection);
        //Debug.Log(originalPos + randomDirection);
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        stoppingDistance = nav.stoppingDistance;

        originalPos = transform.position;
        if(enemyType == 1) {
            InvokeRepeating("Wandering", 0.0f, 3.0f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(enemyType != 1) {
            if (target != null) {
                bool inRange = Vector3.Distance(transform.position, target.position) <= stoppingDistance;           
                if (inRange) {
                    LookAtTarget();
                } else {
                    TowardsTarget();
                }
            }
        }
        else {
            if(Vector3.Distance(transform.position, target.position) <= alertRadius) { TowardsTarget(); }
        }
    }
}


