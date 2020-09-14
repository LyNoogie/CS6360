using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraReset : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("position:"+ this.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("tab"))
        {
            Debug.Log("from position:" + this.transform.position);
            this.transform.position = new Vector3(0f, 0f, 0f);
        }
    }
}
