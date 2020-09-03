using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public Light light;
    private Color[] colorList= { Color.white, Color.red, Color.blue, Color.green}; 
    private int colorIndex = 0;
    // private bool light_switch = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
         if(Input.GetKeyDown("tab"))
         {
            //Debug.Log("change light");
            colorIndex = (colorIndex + 1) % colorList.Length;
            light.color = colorList[colorIndex];
            //light_switch = !light_switch;
            //light.enabled = light_switch;
         }   
    }
}
