using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SetTime : MonoBehaviour
{
    public TextMeshProUGUI time;
    void Start()
    {
        string elapsedTime = PlayerPrefs.GetString("ElapsedTime");

        time.text = elapsedTime;
    }
}
