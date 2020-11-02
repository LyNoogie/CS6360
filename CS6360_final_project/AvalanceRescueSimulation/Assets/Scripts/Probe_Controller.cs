using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Probe_Controller : MonoBehaviour
{
    Vector3 probePos;
    float pPosX;
    float pPosZ;
    Vector3 burriedPos;
    bool withinRange;
    flashImage fi;

    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        probePos = new Vector3(player.position.x, player.transform.position.y, player.transform.position.z ) + player.transform.forward;
        this.transform.rotation = player.transform.rotation;
        withinRange = false;
        fi = GameObject.Find("FlashCanvas").GetComponent<flashImage>();
    }

    // Update is called once per frame
    void Update()
    {
        burriedPos = transmitting_script.beaconPos;

        if (Input.GetKeyDown("z"))
        {
            CheckDistToSource(probePos);
            if(withinRange)
            {
                // Add jitter effect here
                Debug.Log("GOT IT");
                fi.StartFlash(0.2f, 1);
            }
        }
    }

    void CheckDistToSource(Vector3 probe)
    {
        double dist = Math.Sqrt(Math.Pow(probe.x - burriedPos.x, 2) + Math.Pow(probe.z - burriedPos.z, 2));
        withinRange = dist < 50 ? true : false;
    }
}
