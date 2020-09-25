using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallDropScript : MonoBehaviour
{
    public Rigidbody rigidbody;
    public GameObject ball;

    void Start() {
        rigidbody = ball.AddComponent<Rigidbody>();
        rigidbody.useGravity = false;
    }


    void OnTriggerEnter(Collider other) {
        rigidbody.useGravity = true;
    }
}
