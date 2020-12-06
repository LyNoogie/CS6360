using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    private float elapsedRunningTime = 0f;
    private float runningStartTime = 0f;
    private float pauseStartTime = 0f;
    private float elapsedPausedTime = 0f;
    private float totalElapsedPausedTime = 0f;
    private bool running = false;
    private bool paused = false;
    private bool display = true;

    private GameObject timerText;

    void Start() 
    {
        timerText = GameObject.Find("Timer-Text");
        Begin();  // Assumes that the Help Menu is shown at game start
    }
    
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            display = !display;
        }
        if (Input.GetKeyDown("h"))
        {
            paused = !paused;
            if (paused)
            {
                Pause();
            }
            else
            {
                Unpause();
            }
        }
        if (running)
        {
            elapsedRunningTime = Time.time - runningStartTime - totalElapsedPausedTime;
        }
        if (paused)
        {
            elapsedPausedTime = Time.time - pauseStartTime;
        }

        timerText.GetComponent<UnityEngine.UI.Text>().text = display ? elapsedRunningTime.ToString("F1") : "";
    }
  
    public void Begin()
    {
        if (!running && !paused)
        {
            runningStartTime = Time.time;
            elapsedRunningTime = 0.0f;
            running = false;
            paused = true;
        }
    }
  
    public void Pause()
    {
        // if (running && !paused)
        // {
        //     running = false;
        //     pauseStartTime = Time.time;
        //     paused = true;
        // }
        pauseStartTime = Time.time;
        running = false;
    }
  
    public void Unpause()
    {
        // if (!running && paused)
        // {
        //     totalElapsedPausedTime += elapsedPausedTime;
        //     running = true;
        //     paused = false;
        // }
        totalElapsedPausedTime += elapsedPausedTime;
        running = true;
    }
  
    public void Reset()
    {
        elapsedRunningTime = 0f;
        runningStartTime = 0f;
        pauseStartTime = 0f;
        elapsedPausedTime = 0f;
        totalElapsedPausedTime = 0f;
        running = false;
        paused = false;
    }
  
    public int GetMinutes()
    {
        return (int)(elapsedRunningTime / 60f);
    }
  
    public int GetSeconds()
    {
        return (int)(elapsedRunningTime);
    }
 
    public float GetRawElapsedTime()
    {
        return elapsedRunningTime;
    }
}
