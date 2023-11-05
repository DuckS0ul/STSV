using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class Cameracontroller : MonoBehaviour
{
    public Vector2 rotation;

    //public Transform ;
    private float x, y;
    public float mouseSensitivity;
    public float cameraRotation;
    public Transform cameraMove;

    public GameObject player;

    //public Transform cameraTransform;
    public float normalDistance = 1.0f;
    public float collisionDistance = 0.5f;
    public LayerMask terrainLayer;

    private void Start()
    {


    }

    private void Update()
    {
        if (!InventorySystem.Instance.isOpen || !CraftingSystem.Instance.isOpen)
        {
            x = rotation.x * mouseSensitivity * Time.deltaTime;
            y = rotation.y * mouseSensitivity * Time.deltaTime;
            cameraRotation -= y;
            
            cameraRotation = Mathf.Clamp(cameraRotation, -80f, 30f);
            player.transform.Rotate(Vector3.up * x);
            cameraMove.transform.localRotation = Quaternion.Euler(cameraRotation, 0, 0);
        }


        RaycastHit hitInfo;
        if (Physics.Raycast(player.transform.position, -player.transform.forward, out hitInfo, 20f))
        {

            //Debug.Log(",Distance:" + Vector3.Distance(hitInfo.point, player.transform.position) + "Nor " + normalDistance);
            //Debug.Log("hit" + hitInfo.collider.name + " , " + hitInfo.point);
            if (Vector3.Distance(hitInfo.point, player.transform.position) < normalDistance)
            {
                //Debug.Log("hit" + hitInfo.collider.name + " , " + hitInfo.point );
                float dis = hitInfo.distance;
                //Vector3 correction = Vector3.Normalize(cameraMove.transform.TransformDirection(Vector3.back)) * dis;
                //cameraMove.transform.position += correction;
                //cameraMove.transform.position = hitInfo.point;
                cameraMove.transform.localPosition = new Vector3(0.5f, 1f, -1f);
            }
            else
            {
                //Debug.Log("camera normal");
                cameraMove.transform.localPosition = new Vector3(0.82f, 1.4f, -2.81f);
            }
        }
    }



        public void OnLook(InputValue value)
    {
        rotation = value.Get<Vector2>();
    }
}