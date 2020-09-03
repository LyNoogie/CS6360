using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomSwitch : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("2")){
            this.transform.position = new Vector3(0.5f, 0.5f, 0.5f);
            Debug.Log(this.transform.position);
        }

        if (Input.GetKeyDown(KeyCode.Escape)){
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
