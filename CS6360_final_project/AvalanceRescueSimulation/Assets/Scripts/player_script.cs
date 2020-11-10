﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public enum LEVEL
    {
        EASY = 0,
        MEDIUM = 1,
        HARD = 2
    }

    public enum SAFTEY_TOOL
    {
        BEACON = 0,
        PROBE = 1,
        SHOVEL = 2,
        NONE = 3
    }

    public enum PLAYER_SPEED
    {
        FAST = 0,
        MEDIUM = 1,
        SLOW = 2
    }


    public int VisibilityParam;
    public LEVEL LevelType;
    public SAFTEY_TOOL CurrentTool;
    public PLAYER_SPEED Speed;
    public bool ShowFlux;

    public GameObject settings_obj;
    public GameObject SnowParticleSystem;
    private bool load_initSetting;



    private GameObject BeaconObj;
    private GameObject ProbeObj;
    private GameObject ShovelObj;


    // Start is called before the first frame update
    void Start()
    {
        VisibilityParam = 10;
        LevelType = LEVEL.EASY;
        CurrentTool = SAFTEY_TOOL.NONE;
        ShowFlux = false;
        load_initSetting = false;

        VisibilityParam = 0;
        LevelType = LEVEL.EASY;
        CurrentTool = SAFTEY_TOOL.NONE;
        ShowFlux = false;
        BeaconObj = GameObject.Find("BeaconContainer");

        //TODO:: uncomment when probe and shovel inported
        ProbeObj = GameObject.Find("ProbeContainer");
        ShovelObj = GameObject.Find("ShovelContainer");

        BeaconObj.SetActive(false);
        ProbeObj.SetActive(false);
        ShovelObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        // set up player object
        // This should happen only once, but it's put in update() 
        // bc I'm not sure when player_script object is in Start() 
        // whether initSetting's varibles are ready 

        if (!load_initSetting)
        {
            // load a default setting if start from this scene directly
            if (!settings_obj.GetComponent<initSettings>().setInStartMenu())
            {
                VisibilityParam = 10;
                Speed = PLAYER_SPEED.FAST;


                Debug.Log("SKIP INIT SETTINGS: VisibilityParam= " + VisibilityParam.ToString() + " user speed= " + Speed.ToString());
            }
            else
            {
                // enter a new scene, load varibles from init setting object
                VisibilityParam = settings_obj.GetComponent<initSettings>().getSnowAmount();
                Speed = (PLAYER_SPEED)settings_obj.GetComponent<initSettings>().getSnowPackType();

                Debug.Log("LOAD INIT SETTINGS: VisibilityParam= " + VisibilityParam.ToString() + " user speed= " + Speed.ToString());
            }

            // set particle systems accordingly
            ParticleSystem.MainModule m  = SnowParticleSystem.GetComponent<ParticleSystem>().main;
            m.maxParticles = (int)(VisibilityParam  * 800);

            // set bool so that it's executed once
            load_initSetting = true;
        }
        bool toggle_tool = false;
        if (Input.GetKeyDown("c"))
        {
            toggle_tool = true;
            Debug.Log("toggle pressed");
        }
        //if (Input.GetJoystickNames().Length >= 2)
        //{
        //    toggle_tool = OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick);

        //}
        if (toggle_tool)
        {
            Debug.Log("TOggled");
            //TODO:: Uncomment as things are filled in;
            switch (CurrentTool)
            {
                case SAFTEY_TOOL.NONE:
                    CurrentTool = SAFTEY_TOOL.BEACON;
                    ShovelObj.SetActive(false);
                    break;
                case SAFTEY_TOOL.BEACON:
                    CurrentTool = SAFTEY_TOOL.PROBE;
                    BeaconObj.SetActive(true);
                    break;
                case SAFTEY_TOOL.PROBE:
                    CurrentTool = SAFTEY_TOOL.SHOVEL;
                    BeaconObj.SetActive(false);
                    ProbeObj.SetActive(true);
                    break;
                case SAFTEY_TOOL.SHOVEL:
                    CurrentTool = SAFTEY_TOOL.NONE;
                    ProbeObj.SetActive(false);
                    ShovelObj.SetActive(true);
                    break;


            }
        }
    }
}
