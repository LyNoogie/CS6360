using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class initSettings : MonoBehaviour
{
    public static int snow_amount = 0;
    public static int snow_type = 0;
    public static int level = 0;

    private static bool set_in_start_menu;

    public Slider snowSlider;
    public Dropdown snowPackType;
    public Dropdown difficulty;


    private string scene_name;

    // Start is called before the first frame update
    void Start()
    {
        scene_name = SceneManager.GetActiveScene().name;

        if (scene_name != "StartMenu")
        {
            Debug.Log("list init parameters: snow amount:" + snow_amount);
        }
        else
        {
            //difficulty = GameObject.Find("Scene_select_dropdown").GetComponent<Dropdown>();
            difficulty.onValueChanged.AddListener(delegate
            {
                Dropdown1ValueChanged(difficulty);
            });

            //snowPackType = GameObject.Find("Snow_pack_dropdown").GetComponent<Dropdown>();
            snowPackType.onValueChanged.AddListener(delegate
            {
                Dropdown2ValueChanged(snowPackType);
            });
            set_in_start_menu = true;
        }
        //if (scene_name != "StartMemu")
        //    Debug.Log(scene_name + " snow_amount:" + snow_amount.ToString() + " snow_type: " + snow_type.ToString());
    }


    // Update is called once per frame
    void Update()
    {
        if (scene_name == "StartMenu")
        {
            snow_amount = (int)snowSlider.value;
        }
    }



    public void LoadScene(string scene_name)
    {


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

    public int getSnowAmount() { return snow_amount; }
    public int getSnowPackType() { return snow_type; }
    public bool setInStartMenu() { return set_in_start_menu; }
}
