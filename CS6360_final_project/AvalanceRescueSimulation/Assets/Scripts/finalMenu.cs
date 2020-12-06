using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finalMenu : MonoBehaviour
{
    private bool enableComponents = false;
    private bool enterMenu = false;
    private Timer timer;
    private Text txt;
    private float rescueTime = 12345.0f;

    // Start is called before the first frame update
    void Start()
    {
        timer = GameObject.Find("Timer-Canvas").GetComponent<Timer>();
        txt = GameObject.Find("FoundTargetText").GetComponent<Text>();
        gameObject.GetComponent<Canvas>().enabled = false;
    }


    // Update is called once per frame
    void Update()
    {
        if (!enterMenu) {
            rescueTime = timer.GetRawElapsedTime();
            txt.text = "You have found the target!\n"+"Press  esc to exit to main menu.\n" + "Time taken: "+rescueTime.ToString("F1")+"sec";
            enterMenu = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
                SceneManager.LoadScene("StartMenu");
    }

}
