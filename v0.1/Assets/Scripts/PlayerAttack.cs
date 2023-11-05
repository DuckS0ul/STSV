using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttack : MonoBehaviour
{
    public GameObject bullet;
    GameObject newBullet;
    public GameObject firePosition;
    public float bulletSpeed;
    public float attackTime;
    public float minShotTime;
    public float minStickTime;
    public float stickAnimationTime;
    public Vector3 targetPoint;//目标位置
    public Camera mCamera;

    public float range;//射线距离

    public GameObject stick;
    public bool onStick;

    public GameObject playerCapsule;
    

    // Start is called before the first frame update
    void Start()
    {
        attackTime = -1;
        onStick = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        stickAction();
        
    }
    public void Fire()
    {
        if (playerCapsule.GetComponent<PlayerAttribute>().nowEP>0){
            Debug.Log("fire");
            if (Time.time - attackTime > minShotTime)
            {
                mCamera = this.GetComponentInChildren<Camera>();
                RaycastHit rayHit;
                if (Physics.Raycast(mCamera.transform.position, mCamera.transform.forward, out rayHit, range))
                {
                    targetPoint = rayHit.point;
                }
                else
                {

                    targetPoint = mCamera.transform.position + (mCamera.transform.forward * range);
                }

                GameObject newBullet = Instantiate(bullet, firePosition.transform.position, this.transform.rotation);
                newBullet.transform.LookAt(targetPoint);

                Rigidbody bulletRb = newBullet.GetComponent<Rigidbody>();
                bulletRb.velocity = newBullet.transform.forward * bulletSpeed;
                attackTime = Time.time;

            }

        }
        
    }
    public void IfStick()
    {
        if (Time.time - attackTime > minStickTime)
        {
            stick.GetComponent<SphereCollider>().enabled = true;
            onStick = true;
            attackTime = Time.time;
            //stick.transform.localRotation = Quaternion.Euler(0, -30, -30);
        }
    }
    public void stickAction()
    {  
        if (onStick)
        {
            
            stick.GetComponent<SphereCollider>().enabled = true;
            stick.transform.localRotation = Quaternion.Euler(0, -90, 0);
            //if (stick.transform.rotation.y > -90)
            //{
            //    stick.transform.Rotate(0, (float)-0.1, (float)-0.1, Space.Self);
            //}
            //else
            //{
            //    onStick = false;
            //    stick.GetComponent<SphereCollider>().enabled = false;
            //    //stick.transform.Rotate(0, (float)0.1, (float)0.1, Space.Self);
            //}
            if (Time.time - attackTime >= stickAnimationTime)
            {
                stick.transform.localRotation = Quaternion.Euler(0, -60, 30);
                onStick = false;
                stick.GetComponent<SphereCollider>().enabled = false;
            }            
        }
        else
        {
            //if (stick.transform.rotation.y < -60)
            //{
            //    stick.transform.Rotate(0, (float)0.1, (float)0.1, Space.Self);
            //}
            stick.transform.localRotation = Quaternion.Euler(0, -60, 30);
        }

    }
}
