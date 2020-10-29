using System.Collections;
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
    private bool load_initSetting;



    // Start is called before the first frame update
    void Start()
    {
        VisibilityParam = 0;
        LevelType = LEVEL.EASY;
        CurrentTool = SAFTEY_TOOL.NONE;
        ShowFlux = false;
        load_initSetting = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!load_initSetting)
        {
            // if enter a new scene, set up player object
            // This should happen only once, but it's put in
            // update() bc I'm not sure when player_script
            // object Start() whether initSetting is alive
            VisibilityParam = settings_obj.GetComponent<initSettings>().getSnowAmount();
            Speed = (PLAYER_SPEED) settings_obj.GetComponent<initSettings>().getSnowPackType();

            load_initSetting = true;
        }
    }
}
