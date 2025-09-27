using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// Modified from John French
// https://gamedevbeginner.com/how-to-make-countdown-timer-in-unity-minutes-seconds/

public class Timer : MonoBehaviour
{
    [SerializeField]public float time;
    public float timeRemaining;
    [SerializeField]public bool isRunning;
    [SerializeField]public bool startAutomatically;
    public Text timeText;

    public Timer(float time, bool _startAutomatically = true) {
        timeRemaining = time;
        startAutomatically = _startAutomatically;
    }


    private void Start()
    {
        if (startAutomatically)
            isRunning = true;
        else
            isRunning = false;
    }

    void Update()
    {
        if (isRunning)
        {
            if (timeRemaining > 0)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                isRunning = false;
                timeRemaining = time;
            }
        }
    }


    void DisplayTime(float timeToDisplay)
    {
        if (timeText) {
            timeToDisplay += 1;

            float minutes = Mathf.FloorToInt(timeToDisplay / 60); 
            float seconds = Mathf.FloorToInt(timeToDisplay % 60);

            timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }
}