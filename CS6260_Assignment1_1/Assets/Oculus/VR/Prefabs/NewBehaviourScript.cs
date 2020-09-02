using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class NewBehaviourScript : MonoBehaviour
{
    public Light light;
    // private bool light_switch = false;

    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        // if(Input.GetKeyDown("tab"))
        // {
        //     Console.WriteLine("Got here");
        //     light_switch = !light_switch;
        //     light.enabled = light_switch;
        // }   
    }
}
