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
        if (player == null)
        {
            Debug.Log("asdfSDf");
        }

        player_trans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //Debug.Log(t.)

    }

    // Update is called once per frame
    void Update()
    {
        //var pos=player.transform.position;
        Transform t = GameObject.FindWithTag("Player").GetComponent<Transform>();

        //float x =player.transform.position.x;
        //float y = player.transform.position.y;
        //float z = player.transform.position.z;
        int min_index = GetClosestVector(player_trans.position.x, player_trans.position.y, player_trans.position.z);

        var current_vec = vectors[min_index];

        float vx = current_vec.x;
        float vz = current_vec.z;
        double vector_degree;

        //In calculations below, 0 should be replaced with worldspace angle/direction of player

        // if vx, vz > 0, vector_degree = 0 + (90 - Math.Atan(vz / vx)*(180/Math.PI))
        if (vx > 0 & vz > 0) 
        {
            vector_degree = 90 - Math.Atan(vz / vx)*(180/Math.PI);
        }
        // if vx > 0, vz < 0, vector_degree = 90 + (Math.Atan(vz / vx)*(180/Math.PI))
        else if(vx > 0 & vz < 0)
        {
            vector_degree = 90 + (Math.Atan(vz / vx)*(180/Math.PI));
        }
        // if vx < 0, vz < 0, vector_degree = 180 + (90 - Math.Atan(vz / vx)*(180/Math.PI))
        else if(vx < 0 & vz < 0)
        {
            vector_degree = 180 + (90 - Math.Atan(vz / vx)*(180/Math.PI));
        }
        // if vx < 0, vz > 0, vector_degree = 270 + (Math.Atan(vz / vx)*(180/Math.PI))
        else 
        {
            vector_degree = 270 + (Math.Atan(vz / vx)*(180/Math.PI));
        }


        // double vector_degree = Math.Atan(vz / vx)*(180/Math.PI); //return degrees

        //Vector3 player_direction = player_trans.eulerAngles;
        Debug.Log(vector_degree);

        // var y_rotation = player_trans.rotation.y;
        // Debug.Log(y_rotation);
        //Debug.Log(vector_degree);


        
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
