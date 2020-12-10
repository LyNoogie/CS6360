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
    private List<GameObject> fluxLines;
    Transform player_trans;
    Transform controller_trans;
    Transform used_transform;

    private OVRPlayerController player;
    private GameObject playerObj = null;
    public static double angle_from_beacon;
    public static float arc_length;
    public static Vector3 beaconPos;

    public static bool outsideRange = false;
    public static bool showFlux = false;

    // Start is called before the first frame update
    public 
    void Start()
    {
        coords = new List<Vector3>();
        vectors = new List<Vector3>();
        arcs = new List<float>();
        fluxLines = new List<GameObject>();

        Vector3 origin = new Vector3(5, 0, 0);
        float rotation = 45;

        LoadData();
        CreateLineGameObjs();
        SetFluxPosition(origin, rotation);
        playerObj = GameObject.FindWithTag("Player");
        player = playerObj.GetComponent<OVRPlayerController>();
        player_trans = GameObject.FindWithTag("Player").GetComponent<Transform>();
        //OVRInput.Update();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            if (!showFlux) {
                DrawFluxLines();
            }
            else {
                RemoveFluxLines();
            }
            showFlux = !showFlux;
        }


        OVRInput.Update();
        if (Input.GetJoystickNames().Length < 2)
        {
            used_transform = GameObject.FindWithTag("Player").GetComponent<Transform>();
        }
        else
        {
            used_transform = GameObject.Find("RightHandAnchor").GetComponent<Transform>();
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

    // Draw all flux lines
    void DrawFluxLines()
    {
        float strength = 100;
        for (int i = 0; i < fluxLines.Count; i++) {
            Color color1 = new Color(1.0f, 0.0f, 0.0f, 0.0f);
            Color color2 = new Color(0.0f, 0.0f, 1.0f, (strength - arcs[i]) / strength);
            Vector3 start = coords[i];   
            GameObject line = fluxLines[i];
            //Debug.DrawLine(new Vector3(start.x, 24 , start.z), new Vector3(coords[i].x + vectors[i].x, 24, coords[i].z + vectors[i].z) , color1, 1000.0f);
            DrawLine(line, new Vector3(start.x, 22.5f, start.z), new Vector3(start.x + vectors[i].x, 22.5f, start.z + vectors[i].z), color1, color2, 1000.0f);
        }
    }

    // Remove all flux lines
    void RemoveFluxLines() {
        for (int i = 0; i < fluxLines.Count; i++) {
            GameObject line = fluxLines[i];
            LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.positionCount = 0;
        }
    }

    // Initialize flux line objs
    void CreateLineGameObjs(){
        for (int i = 0; i < coords.Count; i++)
        {
            GameObject myLine = new GameObject();
            myLine.AddComponent<LineRenderer>();
            fluxLines.Add(myLine);
        }
    } 

    // Draw a single flux line
    void DrawLine(GameObject line, Vector3 start, Vector3 end, Color color1, Color color2, float duration = 0.2f)
        {
            line.transform.position = start;
             LineRenderer lr = line.GetComponent<LineRenderer>();
            lr.positionCount = 2;
            lr.material = new Material(Shader.Find("Sprites/Default"));
            lr.startColor = color1;
            lr.endColor = color2;
            lr.startWidth = 0.05f;
            lr.endWidth = 0.05f;
            lr.SetPosition(0, start);
            lr.SetPosition(1, end);
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
        //beaconPos = new Vector3(UnityEngine.Random.Range(30, 115), 22, UnityEngine.Random.Range(-40, 60));
        beaconPos = new Vector3(70.0f, 22.0f, 0.0f);
        //beaconPos = new Vector3(0, 22, 0);
        //float randRot = UnityEngine.Random.Range(0, 360);
        float randRot = 270;
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
        double dist = Math.Sqrt(Math.Pow(t.position.x - beaconPos.x, 2) + Math.Pow(t.position.z - beaconPos.z, 2));
        outsideRange = dist > 55 ? true : false;
    }
}
