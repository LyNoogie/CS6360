using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
// using System.Numerics;
using System;



public class transmitting_script : MonoBehaviour
{
    private List<Vector3> coords;
    private List<Vector3> vectors;
    private List<float> arcs;
    Transform player_trans;

    private OVRPlayerController player;
    private GameObject playerObj = null;
    // Start is called before the first frame update
    public 
    void Start()
    {
        coords = new List<Vector3>();
        vectors = new List<Vector3>();
        arcs = new List<float>();
        LoadData();
        Debug.Log(coords.Count);
        Debug.Log(vectors.Count);
        Debug.Log(arcs.Count);
        TransformData();
        CreateArrows();
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<OVRPlayerController>();
        player_trans = GameObject.FindWithTag("Player").GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Transform t = GameObject.FindWithTag("Player").GetComponent<Transform>();
        int min_index = GetClosestVector(player_trans.position.x, player_trans.position.y, player_trans.position.z);
        Vector3 closest_vector = vectors[min_index];
        float arc_length = arcs[min_index];
        float vx = closest_vector.x;
        float vz = closest_vector.z;
        double vector_degree;

        if (vx > 0 & vz > 0) 
        {
            vector_degree = 90 - Math.Atan(vz / vx)*(180/Math.PI);
        }
        else if(vx > 0 & vz < 0)
        {
            vector_degree = 90 + (Math.Atan(Math.Abs(vz / vx))*(180/Math.PI));
        }
        else if(vx < 0 & vz < 0)
        {
            vector_degree = 180 + (90 - Math.Atan(vz / vx)*(180/Math.PI));
        }
        else 
        {
            vector_degree = 270 + (Math.Atan(Math.Abs(vz / vx))*(180/Math.PI));
        }

        Vector3 player_direction = player_trans.eulerAngles;
        double angle_from_beacon = vector_degree - player_direction.y;

        if (angle_from_beacon < -180) {
            angle_from_beacon = 360 - Math.Abs(angle_from_beacon);
        } 
        else if (angle_from_beacon > 180) {
            angle_from_beacon = (360 - angle_from_beacon) * -1;
        }

        Debug.Log("Angle of signal direction from beacon: " + angle_from_beacon);
        Debug.Log("Distance to source: " + arc_length);
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
            Debug.DrawLine(new Vector3(start.x, 0.0f , start.z), new Vector3(coords[i].x + vectors[i].x, 0.0f, coords[i].z + vectors[i].z) , color, 1000.0f);
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

    void TransformData() //
    {

    }
}
