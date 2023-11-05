using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShoot : MonoBehaviour
{
    public Transform shootPoint;
    public LayerMask layerMask;

    private float stoppingDistance;
    public NavMeshAgent nav;
    public Transform target;

    public TrailRenderer fireTrail;
    public List<TrailRenderer> aliveTrails;

    public float shootDelay = 0.2f;
    public float shootDeadline;

    public GameObject player;
    private PlayerAttribute PACs;
    public float fireDamage;

    public Vector3 GetDirection() {
        Vector3 direction = transform.forward;    // z direction
        direction += new Vector3( Random.Range(-0.06f, 0.06f), Random.Range(-0.05f, 0.05f), Random.Range(-0.07f, 0.07f) );
        direction.Normalize();                    // length = 1
        return direction;
    }

    // Coroutine
    private IEnumerator GenerateTrail(TrailRenderer trail, RaycastHit hit) {
        float time = 0f;
        Vector3 startPosition = trail.transform.position;

        while (time < 1f) {
            trail.transform.position = Vector3.Lerp(startPosition, hit.point, time);
            time += Time.deltaTime / trail.time;

            yield return null;  // stop this coroutine until next frame
        }

        trail.transform.position = hit.point;
        Destroy(trail.gameObject);
        aliveTrails.Remove(trail);
        PACs.GetHurt(fireDamage);
    }


    public void Shoot() {
        Vector3 direction = GetDirection();
        // layerMask = everything   if raycast hit anything it's ture
        if ( Physics.Raycast(shootPoint.position, direction, out RaycastHit hit, float.MaxValue, layerMask) ) {
            TrailRenderer trail = Instantiate(fireTrail, shootPoint.position, Quaternion.identity);
            aliveTrails.Add(trail);
            StartCoroutine(GenerateTrail(trail, hit));
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        nav = GetComponent<NavMeshAgent>();
        stoppingDistance = nav.stoppingDistance;
        PACs = player.GetComponent<PlayerAttribute>();
        aliveTrails = new List<TrailRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        bool inRange = Vector3.Distance(transform.position, target.position) <= stoppingDistance;            
        if (inRange) {
            if ( Time.time >= shootDeadline ){
                shootDeadline = Time.time + shootDelay;
                Shoot();
            }
        }        
    }

    void OnDestroy() {
        foreach (var trail in aliveTrails) {
            if(trail != null) { Destroy(trail.gameObject); }
        }
    }
}
