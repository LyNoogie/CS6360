using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.XR;

public class Probe_Controller : MonoBehaviour
{
    Vector3 probePos;
    float pPosX;
    float pPosZ;
    Vector3 burriedPos;
    bool withinRange;
    flashImage fi;
    public float pForward;
    public float pHeight = -0.3f;
    public bool pEquip;
    public bool isProbing;
    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        probePos = new Vector3(player.position.x, player.transform.position.y + pHeight, player.transform.position.z) + player.transform.forward * pForward;
        this.transform.rotation = player.transform.rotation;
        withinRange = false;
        fi = GameObject.Find("FlashCanvas").GetComponent<flashImage>();
        pEquip = false;
        pForward = 0.6f;
        isProbing = false;
    }    // Update is called once per frame
    void Update()
    {
        if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) > 0)
        {
            //this.transform.position = new Vector3(player.position.x, player.position.y + pHeight, player.position.z - 30f) + player.forward * pForward;
            isProbing = true;
        }        //if (OVRInput.GetUp(OVRInput.Axis1D.SecondaryIndexTrigger))
        //{
        //    //this.transform.position = new Vector3(player.position.x, player.position.y + pHeight, player.position.z) + player.forward * pForward;
        //    isProbing = false;
        //}
        if (Input.GetKeyDown("z"))
        {
            isProbing = true;
        }
        if (Input.GetKeyUp("z"))
        {
            isProbing = false;
        }
        Transform player = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        if (isProbing)
        {
            probePos = new Vector3(player.position.x, player.position.y + pHeight - 2f, player.position.z) + player.forward * pForward;
        }
        else
        {
            probePos = new Vector3(player.position.x, player.position.y + pHeight, player.position.z) + player.forward * pForward;
        }
        //this.transform.rotation = player.transform.rotation;
        this.transform.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, player.rotation.eulerAngles.y + 90f, player.rotation.eulerAngles.z);
        this.transform.position = probePos;
        //this.GetComponent<Renderer>().enabled = pEquip;
        burriedPos = transmitting_script.beaconPos;        //bool is_triggered = false;
        ////if (Input.GetJoystickNames().Length < 2)
        ////{
        //    is_triggered = OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);        //}
        //else
        //{
        //}
        if (isProbing)
        {
            pEquip = true;
            CheckDistToSource(probePos);
            if (withinRange)
            {
                // Add jitter effect here
                Debug.Log("GOT IT");
                fi.StartFlash(0.2f, 1);
            }
        }
        isProbing = false;
    }
    void CheckDistToSource(Vector3 probe)
    {
        double dist = Math.Sqrt(Math.Pow(probe.x - burriedPos.x, 2) + Math.Pow(probe.z - burriedPos.z, 2));
        withinRange = dist < 1 ? true : false;
    }
}