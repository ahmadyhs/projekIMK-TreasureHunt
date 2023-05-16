using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI timer;
    private float startTime;
   

    void Start()
    {
        startTime = Time.time;
  
    }

    void Update()
    {
       
            float t = Time.time - startTime;

            string minutes = ((int)t / 60).ToString();
            string seconds = (t % 60).ToString("f2");

            timer.text = minutes + ":" + seconds;
    
    }
}
