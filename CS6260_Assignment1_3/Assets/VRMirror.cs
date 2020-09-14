using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VRMirror : MonoBehaviour
{
    public GameObject playerObj = null;
    private OVRPlayerController player;

    private Vector3 lastFramePlayPos;
    private Vector3 lastFramePlayRot;
    private Vector3 initialPlayPos;
    private Vector3 initialPlayRot;
    private bool updatePosition = true;
    private bool updateRotation = true;

    private enum mode {MATCH=0, MIRROR=1};
    private mode Mode = mode.MATCH;

    private bool debugPrint = false;

    // Start is called before the first frame update
    void Start()
    {

        player = playerObj.GetComponent<OVRPlayerController>();
        lastFramePlayPos = player.transform.position;
        lastFramePlayRot = player.transform.eulerAngles;
        initialPlayPos = player.transform.position;
        initialPlayRot = player.transform.eulerAngles;

        Debug.Log("Start in Match Mode");
    }

    // Update is called once per frame
    void Update()
    {
        // prcess input keys
        if (Input.GetKeyDown("m")) {
            Mode = 1 - Mode;
            lastFramePlayRot += new Vector3(0, 180, 0);// flip between modes
            Debug.Log("Switch to" + ((Mode == mode.MATCH)?"Match":"Mirror") +" Mode");
        }

        if (Input.GetKeyDown("b")){
            updatePosition = false;
            updateRotation = false;
        }

        if (Input.GetKeyDown("r")) updateRotation = !updateRotation;
        if (Input.GetKeyDown("p")) updatePosition = !updatePosition;

        if (!(updateRotation || updatePosition)) return;

        // update guard 
        if (Mode == mode.MATCH)
        {
            if (updatePosition && (player.transform.position != lastFramePlayPos))
            {
                transform.position += player.transform.position - lastFramePlayPos;
                if (debugPrint) Debug.Log("Match position:" + transform.position + " " + player.transform.position);

                lastFramePlayPos = player.transform.position;
            }

            if (updateRotation && (player.transform.eulerAngles != lastFramePlayRot))
            {
                transform.eulerAngles = player.transform.eulerAngles - initialPlayRot;
                if (debugPrint) Debug.Log("Match rotation:" + transform.rotation.eulerAngles + " " + player.transform.eulerAngles);

                lastFramePlayRot = player.transform.eulerAngles;
            }
        }

        if (Mode == mode.MIRROR) {
            if (updatePosition && (player.transform.position != lastFramePlayPos))
            {
                // decompose moved vector for mirroring action
                Vector3 movedV = player.transform.position - lastFramePlayPos;

                // reverse forward/backwards vector compoment
                Vector3 forwardUnitV = player.transform.forward;
                Vector3 forwardV = Vector3.Project(movedV, forwardUnitV);
                transform.position -= forwardV;

                // perserve L/R movement
                transform.position += (movedV - forwardV);
                if (debugPrint) Debug.Log("Mirrior position:" + transform.position + " " + player.transform.position);

                lastFramePlayPos = player.transform.position;
            }

            if (updateRotation && (player.transform.eulerAngles != lastFramePlayRot))
            {
                transform.eulerAngles = -(player.transform.eulerAngles - initialPlayRot) + new Vector3(0, 180, 0);
                if (debugPrint) Debug.Log("Mirrir rotation:" + transform.rotation.eulerAngles + " " + player.transform.eulerAngles);

                lastFramePlayRot = player.transform.eulerAngles;
            }
        }
    }
}
