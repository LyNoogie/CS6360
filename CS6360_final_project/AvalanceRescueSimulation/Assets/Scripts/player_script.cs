using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_script : MonoBehaviour
{
    public enum LEVEL
    {
        EASY=0,
        MEDIUM=1,
        HARD=2
    }

    public enum SAFTEY_TOOL
    {
        BEACON=0,
        PROBE=1,
        SHOVEL=2,
        NONE=3
    }


    public int VisibilityParam;
    public LEVEL LevelType;
    public SAFTEY_TOOL CurrentTool;
    public bool ShowFlux;

    private GameObject BeaconObj;
    private GameObject ProbeObj;
    private GameObject ShovelObj;



    // Start is called before the first frame update
    void Start()
    {
        VisibilityParam = 0;
        LevelType = LEVEL.EASY;
        CurrentTool = SAFTEY_TOOL.NONE;
        ShowFlux = false;
        BeaconObj = GameObject.Find("BeaconContainer");

        //TODO:: uncomment when probe and shovel inported
        //ProbeObj = GameObject.Find("ProbeContainer");
        //ShovelObj = GameObject.Find("ShovelContainer");


    }

    // Update is called once per frame
    void Update()
    {
        bool toggle_tool = false;
        
        if (Input.GetJoystickNames().Length < 2)
        {

        }
        else
        {
            toggle_tool = OVRInput.GetUp(OVRInput.Button.SecondaryThumbstick);
        }
        if (toggle_tool)
        {
            Debug.Log("TOggled");
            //TODO:: Uncomment as things are filled in;
            switch(CurrentTool){
                case SAFTEY_TOOL.NONE:
                    CurrentTool = SAFTEY_TOOL.BEACON;
                    //ShovelObj.SetActive(false);
                    break;
                case SAFTEY_TOOL.BEACON:
                    CurrentTool = SAFTEY_TOOL.PROBE;
                    BeaconObj.SetActive(true);
                    break;
                case SAFTEY_TOOL.PROBE:
                    CurrentTool = SAFTEY_TOOL.SHOVEL;
                    BeaconObj.SetActive(false);
                    //ProbeObj.SetActive(true);
                    break;
                case SAFTEY_TOOL.SHOVEL:
                    CurrentTool = SAFTEY_TOOL.NONE;
                    //ProbeObj.SetActive(false);
                    //ShovelObj.SetActive(true);
                    break;

                    
            }
        }

    }
}
