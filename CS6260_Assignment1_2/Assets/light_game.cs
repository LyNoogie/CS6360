using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class light_game : MonoBehaviour
{
    private double time;
    //public GameObject playerObj = null;
    //private OVRPlayerController player;

    public GameObject[] lightObjs;
    public GameObject score_text_obj;
    private float timer;
    private int currentLight;
    private int minIndex=0;
    private int maxIndex=4;
    private int score;
    private Text score_text;
    private Text instructions;
   

    public void updateScore()
    {
        score++;
        Debug.Log("Score is " + score);
        Debug.Log("current light is " + currentLight);
        lightObjs[currentLight].GetComponent<Light>().enabled = false;
        currentLight = Random.Range(minIndex, maxIndex);
        lightObjs[currentLight].GetComponent<Light>().enabled = true;
        timer = 0;
        score_text.text = score.ToString();

    }

    // Start is called before the first frame update
    void Start()
    {
        score_text= GameObject.FindGameObjectsWithTag("score_text")[0].GetComponent<Text>();
        score_text.text = "0";
        //player = playerObj.GetComponent<OVRPlayerController>();
        score = 0;
        lightObjs = GameObject.FindGameObjectsWithTag("trig_light");
        Debug.Log(lightObjs.Length);

        currentLight = Random.Range(minIndex, maxIndex);

        for (int i = 0; i < maxIndex; i++)
        {
            lightObjs[i].GetComponent<Light>().enabled = false;
        }
        lightObjs[currentLight].GetComponent<Light>().enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > 3.0f)
        {
            lightObjs[currentLight].GetComponent<Light>().enabled = false;
            currentLight = Random.Range(minIndex, maxIndex);
            lightObjs[currentLight].GetComponent<Light>().enabled = true;
            timer = 0;
        }

        //Todo: exit button
    }

  
}
