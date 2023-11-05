using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using JetBrains.Annotations;
//using static UnityEditor.Timeline.TimelinePlaybackControls;

public class PlayerAttribute : MonoBehaviour
{
    // Start is called before the first frame update
    public static float HP;
    public float nowHP;
    public Slider HPbar;
    public static float EP;
    public float nowEP;
    public Slider EPbar;
    public float SP;
    public float nowSP;
    public Slider SPbar;
    public Image HPbuffer;

    public float RunEPPerFrame = 0.08f;
    public float EPRestorePerFrame = 0.16f;
    public float EPRestoreDelay = 0.5f;
    public float AttackEP = 25f;
    public float SPPerTime = 1f;
    public float SpeedUp = 1.6f;
    public float JumpEp = 20f;
    public float MeleeEP = 30f;


    private float timer;
    public bool restore;

    public GameObject parent;
    private PlayerControl PMcs;

    public GameObject HPlinesAbove;
    public GameObject HPlinesRight;
    public GameObject HPlinesBottom;

    public GameObject EPlinesAbove;
    public GameObject EPlinesRight;
    public GameObject EPlinesBottom;

    public GameObject SPlinesAbove;
    public GameObject SPlinesRight;
    public GameObject SPlinesBottom;

    public bool ifRun;
    public GameObject PauseMenu;
    //public GameObject PauseText;
    public GameObject ESCSystem;
    public GameObject DeadMenu;
    public GameObject WinMenu;


    void Awake()
    {
        Time.timeScale = 1f;
        HP = 300;
        EP = 200;
        SP = 250;
    }
    void Start()
    {
  
        HPBarFresh();
        EPBarFresh();
        SPBarFresh();
        timer = 0;
        restore = false;
        PMcs = parent.GetComponent<PlayerControl>();
        ifRun = false;
    }

    // Update is called once per frame
    void Update()
    {
        HPbar.value = nowHP;
        EPbar.value = nowEP;
        SPbar.value = nowSP;

       
        //Restore EP
        if( restore && Time.time > timer + EPRestoreDelay)
        {
            Restore();
        }

        if (ifRun)
        {
            Run();
        }
      
    }



    public void Pause()
    {
        if (Time.timeScale == 1)
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            PauseMenu.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void PauseNoUI()
    {
        Time.timeScale = 0;
    }

    public void ResumeNoUI()
    {
        Time.timeScale = 1;
    }
    void FixedUpdate()
    {
        if (HPbuffer.fillAmount > nowHP / HP)
        {
            HPbuffer.fillAmount -= 0.002f * HPbuffer.fillAmount * HP * HPbuffer.fillAmount * HP / (nowHP*nowHP);
        }
        SPConsumption();

        if (nowHP == 0||nowSP==0)
        {
            Dead();
            
        }
    }

    public void GetHurt(float damage)
    {
        if (nowHP - damage > 0)
        {
            nowHP -= damage;
        }
        else
        {
            nowHP = 0;
            //Debug.Log("¼Ä");
        }
    }


    public static void EnergyConsumption()
    {

    }

    private void HPBarFresh()
    {
        HPbar.maxValue = HP;
        nowHP = HP;
        HPbuffer.fillAmount = 1;
        RectTransform HPbarTrans = HPbar.GetComponent<RectTransform>();
        HPbarTrans.sizeDelta = new Vector2(HP,10);
        HPbarTrans.anchoredPosition3D = new Vector3(HP / 2 + 10, -15, 0);
        HPlinesAbove.GetComponent<RectTransform>().sizeDelta = new Vector2(HP + 2, 1);
        HPlinesAbove.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(HP / 2 + 10, -9.5f, 0);

        HPlinesBottom.GetComponent<RectTransform>().sizeDelta = new Vector2(HP + 2, 1);
        HPlinesBottom.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(HP / 2 + 10, -20.5f, 0);

        HPlinesRight.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(HP + 10.5f, -15f, 0);
    }

    private void EPBarFresh()
    {
        EPbar.maxValue = EP;
        nowEP = EP;
        RectTransform EPbarTrans = EPbar.GetComponent<RectTransform>();
        EPbarTrans.sizeDelta = new Vector2(EP, 10);
        EPbarTrans.anchoredPosition3D = new Vector3(EP / 2 + 10, -26.5f, 0);

        EPlinesAbove.GetComponent<RectTransform>().sizeDelta = new Vector2(EP + 2, 1);
        EPlinesAbove.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(EP / 2 + 10, -21f, 0);

        EPlinesBottom.GetComponent<RectTransform>().sizeDelta = new Vector2(EP + 2, 1);
        EPlinesBottom.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(EP / 2 + 10, -32f, 0);

        EPlinesRight.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(EP + 10.5f, -26.5f, 0);
    }

    private void SPBarFresh()
    {
        SPbar.maxValue = SP;
        nowSP = SP;
        RectTransform SPbarTrans = SPbar.GetComponent<RectTransform>();
        SPbarTrans.sizeDelta = new Vector2(SP, 10);
        SPbarTrans.anchoredPosition3D = new Vector3(SP / 2 + 10, -38, 0);

        SPlinesAbove.GetComponent<RectTransform>().sizeDelta = new Vector2(SP + 2, 1);
        SPlinesAbove.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(SP / 2 + 10, -32.5f, 0);

        SPlinesBottom.GetComponent<RectTransform>().sizeDelta = new Vector2(SP + 2, 1);
        SPlinesBottom.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(SP / 2 + 10, -43.5f, 0);

        SPlinesRight.GetComponent<RectTransform>().anchoredPosition3D = new Vector3(SP + 10.5f, -38f, 0);
    }
    
    public void Run()
    {
        restore = false;
        if (nowEP > 0)
        {
            PMcs.speedUp = SpeedUp;
            nowEP -= RunEPPerFrame;
        }
        else
        {
            nowEP = 0;
            PMcs.speedUp = 1f;
        }
       
    }

    private void Restore()
    {
        if(restore && Time.timeScale == 1)
        {
            if (nowEP <= EP - EPRestorePerFrame)
            {
                nowEP += EPRestorePerFrame;
            }
            else
            {
                nowEP = EP;
                restore = false;
            }
        }
    }

    public void Fire()
    {
        restore = false;
        if (nowEP > AttackEP)
        {
            parent.GetComponent<PlayerAttack>().Fire();
            nowEP -= AttackEP;
        }
        else
        {
            parent.GetComponent<PlayerAttack>().Fire();
            nowEP = 0;
        }
    }

    public void AfterFire()
    {
        timer = Time.time;
        restore = true;
    }

    private void SPConsumption()
    {
        if (nowSP > SPPerTime)
        {
            nowSP -= SPPerTime;
        }
        else
        {
            nowSP = 0;
            Debug.Log("¶öËÀÁË£¡");
        }
    }

    public void Eat(float food)
    {
        if(nowSP < SP - food)
        {
            nowSP += food;
        }
        else
        {
            nowSP = SP;
        }
    }

    public void SpeedUpFinish()
    {
        PMcs.speedUp = 1f;
        timer = Time.time;
        restore = true;
        ifRun = false;
    }

    public void Jump()
    {
        restore = false;
        if (nowEP > JumpEp)
        {
            nowEP -= JumpEp;
        }
        else
        {
            nowEP = 0;
        }
    }

    public void Dead()
    {
        PauseNoUI();
        PMcs.playerControl.Player.Disable();
        PMcs.playerControl.UI.Enable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        DeadMenu.SetActive(true);
    }

    public void Win()
    {
        PauseNoUI();
        PMcs.playerControl.Player.Disable();
        PMcs.playerControl.UI.Enable();
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
        WinMenu.SetActive(true);
    }

    public void MeleeAttack()
    {
        if (!parent.GetComponent<PlayerAttack>().onStick)
        {
            restore = false;
            if (nowEP > MeleeEP)
            {
                parent.GetComponent<PlayerAttack>().IfStick();
                nowEP -= MeleeEP;
            }
            else
            {
                parent.GetComponent<PlayerAttack>().IfStick();
                nowEP = 0;
            }
        }
    }
}


