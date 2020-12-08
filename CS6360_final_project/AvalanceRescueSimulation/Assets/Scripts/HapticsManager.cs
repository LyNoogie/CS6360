using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HapticsManager : MonoBehaviour
{
    public static HapticsManager singleton;
    // Start is called before the first frame update
    void Start()
    {
        if(singleton && singleton != this)
            Destroy(this);
        else
            singleton = this;
        Debug.Log("Got here");
        
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRInput.Controller controller)
    {
        Debug.Log("Got here");
        OVRHapticsClip clip = new OVRHapticsClip(vibrationAudio);
        OVRHaptics.RightChannel.Preempt(clip);
    }
}
