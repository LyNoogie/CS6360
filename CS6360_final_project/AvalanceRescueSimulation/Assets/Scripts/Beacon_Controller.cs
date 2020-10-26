﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon_Controller : MonoBehaviour
{

    public Vector3 beaconVec;
    public float beaconMag;
    public Quaternion beaconAngle;
    public GameObject player;
    public float bOffset;

    GameObject b1;
    GameObject b2;
    GameObject b3;
    GameObject b4;
    GameObject b5;
    GameObject text;
    Material litMat;
    Material unlitMat;

    // Start is called before the first frame update
    void Start()
    {

        player = GameObject.Find("OVRCameraRig");
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y - 0.2f, player.transform.position.z) + player.transform.forward*bOffset;
        this.transform.rotation = player.transform.rotation;
        this.transform.Rotate(0f, 0f, 180f);

        Vector3 testv = new Vector3(2.0f,1.0f,1.0f);
        litMat = Resources.Load("Lit", typeof(Material)) as Material;
        unlitMat = Resources.Load("Unlit", typeof(Material)) as Material;

        text = GameObject.Find("BeaconText");
        b1 = GameObject.Find("B1");
        b2 = GameObject.Find("B2");
        b3 = GameObject.Find("B3");
        b4 = GameObject.Find("B4");
        b5 = GameObject.Find("B5");
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * bOffset;
        this.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
        //this.transform.Rotate(0f, 180f, 0f);

        float yAngle = this.transform.rotation.eulerAngles.y - beaconAngle.eulerAngles.y;
        //float tAngle = beaconAngle.eulerAngles.y;


        b1.GetComponent<Renderer>().material = unlitMat;
        b2.GetComponent<Renderer>().material = unlitMat;
        b3.GetComponent<Renderer>().material = unlitMat;
        b4.GetComponent<Renderer>().material = unlitMat;
        b5.GetComponent<Renderer>().material = unlitMat;

        if (yAngle <= -60f)
        {
            b1.GetComponent<Renderer>().material = litMat;
        }
        else if (yAngle <= -30f && yAngle > -60f)
        {
            b2.GetComponent<Renderer>().material = litMat;
        }
        else if (yAngle >= -30f && yAngle < 30f)
        {
            b3.GetComponent<Renderer>().material = litMat;
        }
        else if (yAngle >= 30f && yAngle < 60f)
        {
            b4.GetComponent<Renderer>().material = litMat;
        }
        else if (yAngle >= 60f)
        {
            b5.GetComponent<Renderer>().material = litMat;
        }

        text.GetComponent<UnityEngine.UI.Text>().text = beaconMag.ToString("F1");
        //switch (yAngle)
        //{
        //    case var _ when yAngle <= -60f:
        //        tb = b1;
        //    case float angle when angle <= -30f && angle > -60f:
        //        tb = b2;
        //    case float angle when angle > -30f && angle < 30f:
        //        tb = b3;
        //    case float angle when angle > 30f && angle < 60f:
        //        tb = b4;
        //    case float angle when angle > 60f:
        //        tb = b5;
        //}
        //tb.GetComponent<Renderer>().material = litMat;


    }
   
    
    
}
