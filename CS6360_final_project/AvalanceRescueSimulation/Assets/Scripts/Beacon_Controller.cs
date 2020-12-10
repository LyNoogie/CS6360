using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class Beacon_Controller : MonoBehaviour
{

    private float bHeight = 0.1f;
    public float beaconMag;
    public Quaternion beaconAngle;
    public GameObject player;
    public float bForward;
    //public bool bEquip;

    
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
        bForward = 1.0f;
        player = GameObject.Find("OVRCameraRig");
        this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y + bHeight - 0.2f, player.transform.position.z ) + player.transform.forward * bForward;
        this.transform.rotation = player.transform.rotation;
        this.transform.Rotate(0f, 0f, 180f);

        Vector3 testv = new Vector3(2.0f,1.0f,1.0f);
        litMat = Resources.Load("Lit", typeof(Material)) as Material;
        unlitMat = Resources.Load("Unlit", typeof(Material)) as Material;

        //bEquip = true;

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
        //OVRInput.Update();

        double signal_angle = transmitting_script.angle_from_beacon;
        float arc_length = transmitting_script.arc_length;
        bool outsideRange = transmitting_script.outsideRange;



        //this.GetComponent<Renderer>().enabled = bEquip;

        if (!XRDevice.isPresent)

            {
                this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * bForward;
            this.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
            //this.transform.Rotate(0f, 180f, 0f);
            //float yAngle = this.transform.rotation.eulerAngles.y - beaconAngle.eulerAngles.y;
            //float tAngle = beaconAngle.eulerAngles.y;
        }
        else
        {
            Transform t= GameObject.Find("RightHandAnchor").GetComponent<Transform>();


            //this.transform.position = t.position;
            this.transform.position = new Vector3(t.transform.position.x, t.transform.position.y, t.transform.position.z);
            this.transform.rotation = Quaternion.Euler(-t.transform.rotation.eulerAngles.x, t.transform.rotation.eulerAngles.y + 180f, -t.transform.rotation.eulerAngles.z);


            //this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * bOffset;
            //this.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);

        }
        b1.GetComponent<Renderer>().material = unlitMat;
        b2.GetComponent<Renderer>().material = unlitMat;
        b3.GetComponent<Renderer>().material = unlitMat;
        b4.GetComponent<Renderer>().material = unlitMat;
        b5.GetComponent<Renderer>().material = unlitMat;

        text.GetComponent<UnityEngine.UI.Text>().text = outsideRange ? "" : arc_length.ToString("F1") ;
        if (outsideRange) {
            return;
        }

        if (signal_angle >= 60f)
        {
            b1.GetComponent<Renderer>().material = litMat;
        }
        else if (signal_angle >= 30f && signal_angle < 60f)
        {
            b2.GetComponent<Renderer>().material = litMat;
        }
        else if (signal_angle >= -30f && signal_angle < 30f)
        {
            b3.GetComponent<Renderer>().material = litMat;
        }
        else if (signal_angle <= -30f && signal_angle > -60f)
        {
            b4.GetComponent<Renderer>().material = litMat;
        }
        else if (signal_angle <= -60f)
        {
            b5.GetComponent<Renderer>().material = litMat;
        }


        

    }


   
    
    
}
