using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicIntensity : MonoBehaviour
{
    private float originalIntensity;
    private Light light;
    private NaturalSineWave sin;
    private float t;
    public float VariationAmount;
    public float MaxAmplitude;
    public float Frequency;
    // Start is called before the first frame update
    void Start()
    {
        light = GetComponent<Light>();
        sin = new NaturalSineWave();
        originalIntensity = light.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        sin.VariationAmount = VariationAmount;
        sin.MaxAmplitude = MaxAmplitude;
        sin.Frequency = Frequency;
        t += Time.deltaTime;
        light.intensity = originalIntensity + sin.Evaluate(t);
    }
}
