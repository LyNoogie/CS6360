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
        //Debug.Log(min_index);

        var current_vec = vectors[min_index];

        float vx = current_vec.x;
        float vy = current_vec.y;

        double vector_degree = Math.Atan(vx / vy)*(180/Math.PI); //return degrees


        var y_rotation = player_trans.rotation.y;
        Debug.Log(y_rotation);
        Debug.Log(vector_degree);


        
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
            Vector3 t = new Vector3(x, y, 0.0f);
            coords.Add(t);

        }
        reader.Close();
        reader = File.OpenText("vectors.txt");
        

        while ((line = reader.ReadLine()) != null)
        {
            string[] items = line.Split(' ');
            float x = float.Parse(items[0]);
            float y = float.Parse(items[1]);
            Vector3 t = new Vector3(x, y, 0.0f);
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
            Color color = new Color(0.0f, 0.0f, 1.0f);     
            var start = coords[i];   
            Debug.DrawLine(new Vector3(start.x, start.y , 0.0f), new Vector3(coords[i].x + vectors[i].x, coords[i].y + vectors[i].y, 0.0f) , color, 1000.0f);
        }
    }
    
    int GetClosestVector(float x, float y, float z) //player coords
    {
        double min_dist = 100000000;
        int index = -1;
        for(int i=0; i < coords.Count; i++)
        {
            if(Math.Sqrt(Math.Pow(x - coords[i].x, 2) + Math.Pow(y - coords[i].y, 2)) < min_dist)
            {
                min_dist = Math.Sqrt(Math.Pow(x - coords[i].x, 2) + Math.Pow(y - coords[i].y, 2));
                index = i;
            }
        }
        return index;
    }

    void TransformData() //
    {

    }
}
