using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    public Slider slider;
    public FloatReference sliderValue;
    private int execIndex = 0;

    private void Reset()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }
    private void Awake()
    {
        //slider.value= volumeValue.Value;
        slider.onValueChanged.SetPersistentListenerState(0, UnityEventCallState.Off);
        slider.value = PlayerPrefs.GetFloat("musicVolume",sliderValue.Value);
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        slider.onValueChanged.Invoke(slider.value);
    }

    private void ValueChangeCheck()
    {
        if (execIndex == 0)
        {
            sliderValue.Variable.SetValue(sliderValue.Value);
            PlayerPrefs.SetFloat("musicVolume", slider.value);
            execIndex = 1;
            slider.onValueChanged.SetPersistentListenerState(0, UnityEventCallState.RuntimeOnly);
            slider.onValueChanged.Invoke(slider.value);
        }
        else
        {
            execIndex = 0;
            slider.onValueChanged.SetPersistentListenerState(0, UnityEventCallState.Off);
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
