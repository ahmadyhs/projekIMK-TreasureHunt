using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private float startTime;
    public float delay = 1f;
    private bool finished=false;

    void Start()
    {
        startTime = Time.time;

    }

    void Update()
    {
        if (finished)
            return;
        
            float t = Time.time - startTime;
        if (elapsedTime < delay)
            return;

        float t = elapsedTime - delay;

        string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timer.text = minutes + ":" + seconds;
    }
    public void StopTimerAndStoreTime()
    {
        finished = true;
        PlayerPrefs.SetString("ElapsedTime", timer.text);
    }

}
