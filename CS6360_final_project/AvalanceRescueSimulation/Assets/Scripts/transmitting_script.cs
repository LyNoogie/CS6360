using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;



public class transmitting_script : MonoBehaviour
{
    private List<Vector3> coords;
    private List<Vector3> vectors;
    private List<float> arcs;
    Transform player_trans;
    Transform controller_trans;
    Transform used_transform;


    private OVRPlayerController player;
    private GameObject playerObj = null;
    public static double angle_from_beacon;
    public static float arc_length;
    public static Vector3 beaconPos;
    public static bool outsideRange = false;

    // Start is called before the first frame update
    public 
    void Start()
    {
        coords = new List<Vector3>();
        vectors = new List<Vector3>();
        arcs = new List<float>();

        Vector3 origin = new Vector3(5, 0, 0);
        float rotation = 45;

        LoadData();
        SetFluxPosition(origin, rotation);
        CreateArrows();
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<OVRPlayerController>();
        player_trans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //OVRInput.Update();
    }

    // Update is called once per frame
    void Update()
    {
        //int min_index;

        OVRInput.Update();
        if (Input.GetJoystickNames().Length < 2)
        {
            used_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        else
        {
            //used_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();

            used_transform = GameObject.Find("RightHandAnchor").GetComponent<Transform>();
            //used_transform.rotation = OVRInput.GetLocalControllerRotation(OVRInput.Controller.RTouch);
            //used_transform.position= OVRInput.GetLocalControllerPosition(OVRInput.Controller.RTouch);
        }
        CheckDistToSource(used_transform);
        if (outsideRange) {
            return;
        }
        int min_index= GetClosestVector(used_transform.position.x, used_transform.position.y, used_transform.position.z);

        Vector3 closest_vector = vectors[min_index];
        arc_length = arcs[min_index];
        float vx = closest_vector.x;
        float vz = closest_vector.z;
        double vector_degree;

        if (vx > 0 & vz > 0) 
        {
            vector_degree = 90 - Math.Atan(vz / vx)*(180/Math.PI);
            //Debug.Log("SAFE");
        }
        else if(vx > 0 & vz < 0)
        {
            vector_degree = 90 + (Math.Atan(Math.Abs(vz / vx))*(180/Math.PI));
            //Debug.Log("DANGER");
        }
        else if(vx < 0 & vz < 0)
        {
            vector_degree = 180 + (90 - Math.Atan(vz / vx)*(180/Math.PI));
            //Debug.Log("SAFE");
        }
        else 
        {
            vector_degree = 270 + (Math.Atan(Math.Abs(vz / vx))*(180/Math.PI));
            //Debug.Log("SAFE");
        }

        Vector3 player_direction = used_transform.eulerAngles;
        angle_from_beacon = vector_degree - player_direction.y;

        if (angle_from_beacon < -180) {
            angle_from_beacon = 360 - Math.Abs(angle_from_beacon);
        } 
        else if (angle_from_beacon > 180) {
            angle_from_beacon = (360 - angle_from_beacon) * -1;
        }
    }

    void LoadData()
    {
        StreamReader reader = File.OpenText("coordinates.txt");
        string line;
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(' ');
            float x = float.Parse(items[0]);
            float y = float.Parse(items[1]);
            Vector3 t = new Vector3(x, 0.0f, y);
            coords.Add(t);

        }
        reader.Close();
        reader = File.OpenText("vectors.txt");
        
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(' ');
            float x = float.Parse(items[0]);
            float y = float.Parse(items[1]);
            Vector3 t = new Vector3(x, 0.0f, y);
            vectors.Add(t);

        }
        reader.Close();
        reader = File.OpenText("arcs.txt");
       
        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(' ');
            float dist = float.Parse(items[0]);
            arcs.Add(dist);
        }
    }

    void CreateArrows()
    {
        for (int i = 0; i < coords.Count; i++)
        {
            Color color;
            var start = coords[i];   
            if (start.x > 0) {
                color = new Color(0.0f, 0.0f, 1.0f);     
            }
            else{
                color = new Color(1.0f, 0.0f, 0.0f);  
            }
            Debug.DrawLine(new Vector3(start.x, 24 , start.z), new Vector3(coords[i].x + vectors[i].x, 24, coords[i].z + vectors[i].z) , color, 1000.0f);
        }
    }
    
    int GetClosestVector(float x, float y, float z) //player coords
    {
        double min_dist = 100000000;
        int index = -1;
        for(int i=0; i < coords.Count; i++)
        {
            if(Math.Sqrt(Math.Pow(x - coords[i].x, 2) + Math.Pow(z - coords[i].z, 2)) < min_dist)
            {
                min_dist = Math.Sqrt(Math.Pow(x - coords[i].x, 2) + Math.Pow(z - coords[i].z, 2));
                index = i;
            }
        }
        return index;
    }

    void SetFluxPosition(Vector3 translation, float rotation) 
    {
        beaconPos = new Vector3(UnityEngine.Random.Range(30, 115), 22, UnityEngine.Random.Range(-40, 60));
        //beaconPos = new Vector3(0, 22, 0);
        float randRot = UnityEngine.Random.Range(0, 360);
        //float randRot = 0;

        for (int i = 0; i < coords.Count; i++)
        {
            // Rotate
            coords[i] = Quaternion.Euler(0, randRot, 0) * coords[i];
            vectors[i] = Quaternion.Euler(0, randRot, 0) * vectors[i];

            // Translate (only have to worry about coords, since vectors will be unaffected by translation)
            coords[i] += beaconPos;
        }     
    }

    void CheckDistToSource(Transform t)
    {
        double dist;
        if (Input.GetJoystickNames().Length < 2)
        {
            dist = Math.Sqrt(Math.Pow(t.position.x - beaconPos.x, 2) + Math.Pow(t.position.z - beaconPos.z, 2));
        }
        else
        {
            dist = Math.Sqrt(Math.Pow(t.position.x - beaconPos.x, 2) + Math.Pow(t.position.z - beaconPos.z, 2));

        }
        outsideRange = dist > 55 ? true : false;
    }
}
