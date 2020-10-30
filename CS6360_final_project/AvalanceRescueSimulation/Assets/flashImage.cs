using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]

public class flashImage : MonoBehaviour
{

    public Image img;
<<<<<<< HEAD
    RectTransform flashCanvasPos;
    Vector3 backupFlashCanvasPos;
    bool vibrating = false;

=======
    public uint numOfStepPerJitter = 1; // increase for softer jitter
>>>>>>> b54f269bd0e510fcb44520a237858e70e7ca06df

    private float currentAlpha;
    private float flashAlhpaDeltaEachStep;
    private int flashStepsToDo;
    private int flashStepsOnMaxAlpha;
    private Vector3 originalImagePos;

    // Start is called before the first frame update
    void Start()
    {
        flashCanvasPos = GameObject.Find("Image").GetComponent<RectTransform>();
        backupFlashCanvasPos = flashCanvasPos.localPosition;

        if (!img) Debug.Log("no imgae");
        currentAlpha = 0.0f;
        flashStepsToDo = 0;

        Color imageColor = img.color;
        imageColor.a = currentAlpha;
        img.color = imageColor;

        originalImagePos = img.transform.position;
    }

    public void StartFlash(float duration, float maxAlpha) {
        if (!img) Debug.Log("no imgae");

        flashAlhpaDeltaEachStep = maxAlpha / (duration * 60);
        flashStepsOnMaxAlpha = (int)(maxAlpha / flashAlhpaDeltaEachStep);
        currentAlpha = 0.0f;
        flashStepsToDo = (int)(2 * maxAlpha / flashAlhpaDeltaEachStep);

        Debug.Log("steps: "+ flashStepsToDo);
    }

    public void Vibrate() {
        float dx = 5 * UnityEngine.Mathf.Sin(10 * flashStepsToDo);
        float dy = 5 * UnityEngine.Mathf.Cos( 8 * flashStepsToDo);
        float xx = flashCanvasPos.localPosition.x;
        float yy = flashCanvasPos.localPosition.y;
        float zz = flashCanvasPos.localPosition.z;
        flashCanvasPos.localPosition = new Vector3(xx + dx, yy + dy, zz);

        if (flashStepsToDo == 0) {
            vibrating = false;
            flashCanvasPos.localPosition = backupFlashCanvasPos;
        }
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            print("k key was pressed");
            StartFlash(0.5f, 1);
            vibrating = true;
        }

        if (vibrating) {
            Vibrate();
        }

        if (flashStepsToDo != 0) {
            if (flashStepsToDo == flashStepsOnMaxAlpha)
                flashAlhpaDeltaEachStep *= -1;

            if (flashStepsToDo - 1 != 0)
                setImageAlpha(img.color.a + flashAlhpaDeltaEachStep);
            else
                setImageAlpha(0.0f);

            if ((flashStepsToDo % numOfStepPerJitter == 0) || (numOfStepPerJitter <= 0))
                jitterImage(10.0f);

            flashStepsToDo--;

        }
    }

    void setImageAlpha(float a) {
        Color imageColor = img.color;
        imageColor.a = a;
        img.color = imageColor;
    }

    void jitterImage(float radius) {
        img.transform.position = originalImagePos + new Vector3(Random.Range(-radius, radius), 
                                                                Random.Range(-radius, radius),
                                                                0);
    }
}
