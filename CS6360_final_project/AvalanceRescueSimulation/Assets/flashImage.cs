using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(Image))]

public class flashImage : MonoBehaviour
{

    public Image img;

    private float currentAlpha;
    private float flashAlhpaDeltaEachStep;
    private int flashStepsToDo;
    private int flashStepsOnMaxAlpha;

    // Start is called before the first frame update
    void Start()
    {
        if (!img) Debug.Log("no imgae");
        currentAlpha = 0.0f;
        flashStepsToDo = 0;

        Color imageColor = img.color;
        imageColor.a = currentAlpha;
        img.color = imageColor;
    }

    public void StartFlash(float duration, float maxAlpha) {
        if (!img) Debug.Log("no imgae");

        flashAlhpaDeltaEachStep = maxAlpha / (duration * 60);
        flashStepsOnMaxAlpha = (int)(maxAlpha / flashAlhpaDeltaEachStep);
        currentAlpha = 0.0f;
        flashStepsToDo = (int)(2 * maxAlpha / flashAlhpaDeltaEachStep);

        Debug.Log("steps: "+ flashStepsToDo);
    }


    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("k"))
        {
            print("k key was pressed");
            StartFlash(0.5f, 1);
        }

        if (flashStepsToDo != 0) {
            if (flashStepsToDo == flashStepsOnMaxAlpha)
                flashAlhpaDeltaEachStep *= -1;


            flashStepsToDo--;

            if (flashStepsToDo != 0)
                setImageAlpha(img.color.a + flashAlhpaDeltaEachStep);
            else
                setImageAlpha(0.0f);

        }
    }

    void setImageAlpha(float a) {
        Color imageColor = img.color;
        imageColor.a = a;
        img.color = imageColor;
    }
}
