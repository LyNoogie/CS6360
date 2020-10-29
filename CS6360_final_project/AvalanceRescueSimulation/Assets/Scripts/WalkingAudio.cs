using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingAudio : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if ( ( Input.GetButtonDown( "Horizontal" ) || Input.GetButtonDown( "Vertical" ) ) && !GetComponent<AudioSource>().isPlaying )
            GetComponent<AudioSource>().Play();
        else if ( !Input.GetButton( "Horizontal" ) && !Input.GetButton( "Vertical" ) && GetComponent<AudioSource>().isPlaying )
            GetComponent<AudioSource>().Stop();
        }
}
