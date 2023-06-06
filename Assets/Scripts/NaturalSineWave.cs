using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NaturalSineWave
{
    public float MaxAmplitude;
    public float Frequency;
    public float VariationAmount;

    public float Evaluate(float time)
    {
        float variation = Random.Range(-VariationAmount, VariationAmount);
        float modifiedAmplitude = MaxAmplitude + variation;
        //float modifiedFrequency = Frequency + variation;

        float normalizedTime = time * Frequency;
        float intensity = modifiedAmplitude * Mathf.Sin(normalizedTime);
        return intensity;
    }
}