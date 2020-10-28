using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class initSettings : MonoBehaviour
{
    public static int snow_amount = 0;
    public static bool hints = false;
    public static float parameter = 0;

    public Slider snowSlider;
    public Slider otherSlider;

    private string scene_name;

    // Start is called before the first frame update
    void Start()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (scene_name != "StartMemu")
            Debug.Log("list init parameters: snow amount:" + snow_amount);
    }


    // Update is called once per frame
    void Update()
    {
        if (scene_name == "StartMemu")
        {
            snow_amount = (int)snowSlider.value;
            parameter = otherSlider.value;
        }
    }

    public void LoadScene(string scene_name)
    {
        SceneManager.LoadScene(scene_name);
    }
}
