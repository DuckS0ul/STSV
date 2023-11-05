using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerControl : MonoBehaviour
{
    public Game playerControl;
    public Vector2 inputDirection;
    public CharacterController characterController;
    public float feed;
    private PlayerAttribute PA;
    public float gravity;
    private Vector3 velocity;
    public float jumpHeight = 3f;

    public Transform player;
    public float groundDistance;
    public LayerMask ground;
    public float speedUp;
    public GameObject playerModel;
    public GameObject GameObjectInventorySystem;
    public GameObject GameObjectCraftSystem;
    private InventorySystem ISCS;
    private CraftingSystem CSCS;
    private GameObject UIscreen;
    public GameObject GameObjectPauseSystem;
    private ESCSystem ESCS;
    
    public bool pick;
    private void Awake()
    {
        playerControl = new Game();
        playerControl.Player.SpeedUp.started += SpeedUpStart;
        playerControl.Player.SpeedUp.performed += SpeedingUP;
        playerControl.Player.SpeedUp.canceled += SpeedUpFinish;

        playerControl.Player.Fire.performed += FirePerform;
        playerControl.Player.Fire.canceled += FireFinish;

        playerControl.Player.Melee.performed += MeleePerform;
        playerControl.Player.Melee.canceled += FireFinish;

        playerControl.Player.Pause.started += EscSystem;
        playerControl.Player.Pause.started += PauseNoUI;
        playerControl.Player.Pause.started += ToggleToUIMap;
        playerControl.Player.Pause.started += ToggleBanCrafSystem;
        playerControl.Player.Pause.started += ToggleBanInvSystem;

        playerControl.Player.InventorySystem.started += InvSystem;
        playerControl.Player.InventorySystem.started += PauseNoUI;
        playerControl.Player.InventorySystem.started += ToggleToUIMap;
        playerControl.Player.InventorySystem.started += ToggleBanCrafSystem;
        playerControl.Player.InventorySystem.started += ToggleBanESCSystem;

        playerControl.Player.CraftingSystem.started += CrafSystem;
        playerControl.Player.CraftingSystem.started += PauseNoUI;
        playerControl.Player.CraftingSystem.started += ToggleToUIMap;
        playerControl.Player.CraftingSystem.started += ToggleBanInvSystem;
        playerControl.Player.CraftingSystem.started += ToggleBanESCSystem;

        playerControl.Player.Pick.started += Pick;
        playerControl.Player.Pick.canceled += Pick;

        playerControl.Player.Jump.started += Jump;
        playerControl.Player.Jump.canceled += JumpFinish;
        

        playerControl.UI.InventorySystem.started += InvSystem;
        playerControl.UI.InventorySystem.started += ToggleToPlayerMap;
        playerControl.UI.InventorySystem.started += ResumeNoUI;
        playerControl.UI.InventorySystem.started += ToggleBanCrafSystem;
        playerControl.UI.InventorySystem.started += ToggleBanESCSystem;

        playerControl.UI.CraftingSystem.started += CrafSystem;
        playerControl.UI.CraftingSystem.started += ToggleBanInvSystem;
        playerControl.UI.CraftingSystem.started += ToggleToPlayerMap;
        playerControl.UI.CraftingSystem.started += ResumeNoUI;
        playerControl.UI.CraftingSystem.started += ToggleBanESCSystem;

        playerControl.UI.Pause.started += EscSystem;
        playerControl.UI.Pause.started += ResumeNoUI;
        playerControl.UI.Pause.started += ToggleToPlayerMap;
        playerControl.UI.Pause.started += ToggleBanCrafSystem;
        playerControl.UI.Pause.started += ToggleBanInvSystem;
        
        
        PA = playerModel.GetComponent<PlayerAttribute>();
        ISCS = GameObjectInventorySystem.GetComponent<InventorySystem>();
        CSCS = GameObjectCraftSystem.GetComponent<CraftingSystem>();
        ESCS = GameObjectPauseSystem.GetComponent<ESCSystem>();

        pick = false;

        playerControl.Player.Enable();
        playerControl.UI.Disable();
    }

    private void MeleePerform(InputAction.CallbackContext obj)
    {
        PA.MeleeAttack();
    }

    private void EscSystem(InputAction.CallbackContext obj)
    {
        //closeOtherMenu();
        ESCS.Toggle();
    }

    void Update()
    {
        inputDirection = playerControl.Player.Move.ReadValue<Vector2>();
        /*
        if (playerControl.Player.enabled)
        {
            Cursor.visible = false; 
        }
        */
    }

    private void Jump(InputAction.CallbackContext obj)
    {
        if (characterController.isGrounded)
        {
            if(PA.nowEP > 0)
            {
                //Debug.Log("on jump");
                PA.Jump();
                velocity.y = Mathf.Sqrt(2f * gravity * jumpHeight);
            }
            
            //velocity.y = 10;
            //velocity.y += gravity * Time.deltaTime;  // dv = g*t
            //characterController.Move(velocity * Time.deltaTime);  // 处理y轴方向的移动
        }
    }

    private void ToggleBanInvSystem(InputAction.CallbackContext obj)
    {
        if (playerControl.UI.InventorySystem.enabled)
        {
            playerControl.UI.InventorySystem.Disable();
        }
        else
        {
            playerControl.UI.InventorySystem.Enable();
        }
    }

    private void ToggleBanCrafSystem(InputAction.CallbackContext obj)
    {
        if (playerControl.UI.CraftingSystem.enabled)
        {
            playerControl.UI.CraftingSystem.Disable();
        }
        else
        {
            playerControl.UI.CraftingSystem.Enable();
        }
    }

    private void ToggleBanESCSystem(InputAction.CallbackContext obj)
    {
        if (playerControl.UI.Pause.enabled)
        {
            playerControl.UI.Pause.Disable();
        }
        else
        {
            playerControl.UI.Pause.Enable();
        }
    }

    private void Pick(InputAction.CallbackContext obj)
    {
        pick = !pick;
    }

    private void CrafSystem(InputAction.CallbackContext obj)
    {
        //closeOtherMenu();
        CSCS.Toggle();
    }

    private void InvSystem(InputAction.CallbackContext obj)
    {
        //closeOtherMenu();
        ISCS.Toggle();
    }

    private void Pause(InputAction.CallbackContext obj)
    {
        PA.Pause();
        //Debug.Log("PLAYER: " + playerControl.Player.enabled);
        //Debug.Log("UI: " + playerControl.UI.enabled);
    }

    private void PauseNoUI(InputAction.CallbackContext obj)
    {
        PA.PauseNoUI();
    }

    private void ResumeNoUI(InputAction.CallbackContext obj)
    {
        PA.ResumeNoUI();
    }

    private void FireFinish(InputAction.CallbackContext obj)
    {
        PA.AfterFire();
    }

    private void JumpFinish(InputAction.CallbackContext obj)
    {
        PA.AfterFire();
    }

    private void FirePerform(InputAction.CallbackContext obj)
    {
        PA.restore = false;
        PA.Fire();
    }

    private void SpeedUpFinish(InputAction.CallbackContext obj)
    {
        PA.SpeedUpFinish();    
    }

    private void SpeedingUP(InputAction.CallbackContext obj)
    {
        if (characterController.isGrounded)
        {
            PA.ifRun = true;
        }
    }

    private void SpeedUpStart(InputAction.CallbackContext obj)
    {
        PA.restore = false;
    }

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        speedUp = 1f;

    }

    // Update is called once per frame
   

    private void FixedUpdate()
    {
        Vector3 move;
        move = transform.right * inputDirection.x + transform.forward * inputDirection.y;
        characterController.Move(speedUp * move * feed * Time.deltaTime);
        if (Physics.CheckSphere(player.position, groundDistance, ground) && velocity.y < 0)
        {
            velocity.y = -2;
        }
        velocity.y -= Time.deltaTime * gravity;
        characterController.Move(velocity * Time.deltaTime);
    }

   
    private void ToggleToUIMap(InputAction.CallbackContext obj)
    {
        playerControl.Player.Disable();
        playerControl.UI.Enable();
        Cursor.visible = true;
    }

    private void ToggleToPlayerMap(InputAction.CallbackContext obj)
    {
        playerControl.Player.Enable();
        playerControl.UI.Disable();
        Cursor.visible = false;
    }
    
    public void ToggleMap(InputAction.CallbackContext obj)
    {
        if (playerControl.Player.enabled)
        {
            playerControl.Player.Disable();
            playerControl.UI.Enable();
            Cursor.visible = true;
        }
        else
        {
            playerControl.Player.Enable();
            playerControl.UI.Disable();
            Cursor.visible = false;
        }
    }
}
