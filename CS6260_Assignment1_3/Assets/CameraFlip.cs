using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFlip : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("f"))
        {
            this.transform.eulerAngles += new Vector3(0, 180, 0);
        }
    }
}
