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
        
    }

    public void TriggerVibration(AudioClip vibrationAudio, OVRPlayerController controller)
    {
        OVRHapticClip clip = new OVRHapticClip(vibrationAudio);
        OVRHaptics.RightChannel.Preempt(clip);
    }
}
