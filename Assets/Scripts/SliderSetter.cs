using RoboRyanTron.Unite2017.Variables;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderSetter : MonoBehaviour
{
    public Slider slider;
    public FloatReference sliderValue;

    private void Reset()
    {
        slider = this.gameObject.GetComponent<Slider>();
    }
    private void Awake()
    {
        slider.value= sliderValue.Value;
    }
    // Start is called before the first frame update
    void Start()
    {
        slider.onValueChanged.Invoke(slider.value);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
