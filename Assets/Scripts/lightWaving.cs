using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class lightWaving : MonoBehaviour
{
    public float speed;
    public float range;
    private Light l;
    float originalIntensity;
    float t = 0f;
    // Start is called before the first frame update
    void Start()
    {
        l = GetComponent<Light>();
        originalIntensity = l.intensity;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;
        l.intensity = originalIntensity +  range * Mathf.Sin(speed * t);
    }
}
