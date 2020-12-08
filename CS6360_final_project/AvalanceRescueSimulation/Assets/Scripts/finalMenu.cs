using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class finalMenu : MonoBehaviour
{
    private bool enableComponents = false;
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

    public void EnterMenu() {
        rescueTime = timer.GetRawElapsedTime();
        txt.text = "You have found the target!\n" + "Press  esc to exit to main menu.\n" + "Time taken: " + rescueTime.ToString("F1") + "sec";
    }

    // Update is called once per frame
    void Update()
    {

    }

}
