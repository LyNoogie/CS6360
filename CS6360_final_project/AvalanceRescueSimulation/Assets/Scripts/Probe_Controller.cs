using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
// using HapticsManager;

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
    public AudioClip thudAudio;
    OVRPlayerController playerController;

    // Start is called before the first frame update
    void Start()
    {
        Transform player = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        probePos = new Vector3(player.position.x, player.transform.position.y + pHeight, player.transform.position.z ) + player.transform.forward * pForward;
        this.transform.rotation = player.transform.rotation;
        withinRange = false;
        fi = GameObject.Find("FlashCanvas").GetComponent<flashImage>();
        pEquip = false;
        pForward = 0.6f;
        playerController = gameObject.GetComponent<OVRPlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        
        Transform player = GameObject.Find("OVRCameraRig").GetComponent<Transform>();
        probePos = new Vector3(player.position.x, player.position.y + pHeight, player.position.z) + player.forward * pForward;
        //this.transform.rotation = player.transform.rotation;
        this.transform.rotation = Quaternion.Euler(player.rotation.eulerAngles.x, player.rotation.eulerAngles.y + 90f, player.rotation.eulerAngles.z);
        this.transform.position = probePos;
        //this.GetComponent<Renderer>().enabled = pEquip;
        burriedPos = transmitting_script.beaconPos;

        bool is_triggered = false;
        if (Input.GetJoystickNames().Length < 2)
        {
            if (Input.GetKeyDown("z"))
            {
                is_triggered = true;
                
            } 
        }
        else
        {
            is_triggered = OVRInput.GetUp(OVRInput.Button.SecondaryIndexTrigger);
        }
        if (is_triggered)
        {
            
            pEquip = true;
            CheckDistToSource(probePos);
            // if (withinRange)
            // {
            fi.StartFlash(0.2f, 1);
            // HapticsManager hm = new HapticsManager();
            HapticsManager.singleton.TriggerVibration(thudAudio, OVRInput.Controller.RTouch);
            GetComponent<AudioSource>().PlayOneShot(thudAudio);
            
            // }
        }


    }

    void CheckDistToSource(Vector3 probe)
    {
        double dist = Math.Sqrt(Math.Pow(probe.x - burriedPos.x, 2) + Math.Pow(probe.z - burriedPos.z, 2));
        withinRange = dist < 50 ? true : false;
    }
}
