using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    AudioSource audioSource;
    public FloatReference volumeValue;
    Slider volumeSlider;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {
        SetVolume();
        //volumeSlider.value = audioSource.volume;
        //volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    //private void OnSliderValueChanged(float value)
    //{
    //    SetVolume(value);
    //}
    public void SetVolume()
    {
        this.SetVolume(volumeValue.Value);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
