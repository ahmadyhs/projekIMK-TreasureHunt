using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeControl : MonoBehaviour
{
    public AudioSource audioSource;
    public Slider volumeSlider;

    private void Start()
    {
        volumeSlider.value = audioSource.volume;
        volumeSlider.onValueChanged.AddListener(OnSliderValueChanged);
    }
    private void OnSliderValueChanged(float value)
    {
        SetVolume(value);
    }
    public void SetVolume(float volume)
    {
        audioSource.volume = volume;
    }
}
