using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class initSettings : MonoBehaviour
{
    public static int snow_amount = 0;
    public static bool hints = false;
    public static float snow_type = 0;
    public static int level = 0;

    public Slider snowSlider;
    public Dropdown snowPackType;
    public Dropdown difficulty;


    private string scene_name;

    // Start is called before the first frame update
    void Start()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (scene_name != "StartMemu")
            Debug.Log("list init parameters: snow amount:" + snow_amount);

        //difficulty = GameObject.Find("Scene_select_dropdown").GetComponent<Dropdown>();
        difficulty.onValueChanged.AddListener(delegate {
            Dropdown1ValueChanged(difficulty);
        });

        //snowPackType = GameObject.Find("Snow_pack_dropdown").GetComponent<Dropdown>();
        snowPackType.onValueChanged.AddListener(delegate {
            Dropdown2ValueChanged(snowPackType);
        });
    }


    // Update is called once per frame
    void Update()
    {
        if (scene_name == "StartMemu")
        {
            snow_amount = (int)snowSlider.value;
            
        }

    }



    public void LoadScene(string scene_name)
    {

        //set up player object


        //todo:: add in scenes for difficulty
        SceneManager.LoadScene("snowScene");
    }
    //Ouput the new value of the Dropdown into Text
    void Dropdown1ValueChanged(Dropdown change)
    {
        level= change.value;
        Debug.Log("level is "+ level.ToString());
    }

    void Dropdown2ValueChanged(Dropdown change)
    {
        snow_type = change.value;
        Debug.Log("level is " + snow_type.ToString());
    }
}
