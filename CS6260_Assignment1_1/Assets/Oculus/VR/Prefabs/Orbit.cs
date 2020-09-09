using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Orbit : MonoBehaviour
{
    public Transform transform;

    // Start is called before the first frame update
    void Start()
    {
        transform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(0.0f, 0.5f, 0.0f, Space.Self);
    }
}
