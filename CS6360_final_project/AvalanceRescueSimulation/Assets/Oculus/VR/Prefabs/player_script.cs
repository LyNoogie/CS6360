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




    // Start is called before the first frame update
    void Start()
    {
        VisibilityParam = 0;
        LevelType = LEVEL.EASY;
        CurrentTool = SAFTEY_TOOL.NONE;
        ShowFlux = false;

        

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
