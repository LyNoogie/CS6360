using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon_Controller : MonoBehaviour
{

    public float bHeight;
    public float beaconMag;
    public Quaternion beaconAngle;
    public GameObject player;
    public float bForward;
    

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

        if (Input.GetJoystickNames()[1] == "")
        {
            Debug.Log("forward: " + player.transform.forward * bForward);
            this.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z) + player.transform.forward * bForward;
            this.transform.rotation = Quaternion.Euler(player.transform.rotation.eulerAngles.x, player.transform.rotation.eulerAngles.y + 180f, player.transform.rotation.eulerAngles.z);
            //this.transform.Rotate(0f, 180f, 0f);
            //float yAngle = this.transform.rotation.eulerAngles.y - beaconAngle.eulerAngles.y;
            //float tAngle = beaconAngle.eulerAngles.y;
        }
        else
        {
            Transform t= GameObject.FindWithTag("RightHandAnchor").GetComponent<Transform>();


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
