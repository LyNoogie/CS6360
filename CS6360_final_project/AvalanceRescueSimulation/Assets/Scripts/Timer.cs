﻿using System.Collections;
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
    private bool display = false;

    private GameObject timerText;

    void Start() 
    {
        timerText = GameObject.Find("Timer-Text");
        Begin();
    }
    
    void Update()
    {
        if (Input.GetKeyDown("t"))
        {
            display = !display;
        }
        if (running)
        {
            elapsedRunningTime = Time.time - runningStartTime - totalElapsedPausedTime;
        }
        else if (paused)
        {
            elapsedPausedTime = Time.time - pauseStartTime;
        }
        Debug.Log(display);

        timerText.GetComponent<UnityEngine.UI.Text>().text = display ? elapsedRunningTime.ToString("F1") : "";
    }
  
    public void Begin()
    {
        if (!running && !paused)
        {
            runningStartTime = Time.time;
            running = true;
        }
    }
  
    public void Pause()
    {
        if (running && !paused)
        {
            running = false;
            pauseStartTime = Time.time;
            paused = true;
        }
    }
  
    public void Unpause()
    {
        if (!running && paused)
        {
            totalElapsedPausedTime += elapsedPausedTime;
            running = true;
            paused = false;
        }
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
