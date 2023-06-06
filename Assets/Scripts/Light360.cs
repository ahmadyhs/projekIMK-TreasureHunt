using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Light360 : MonoBehaviour
{
    public Quaternion rotTarget = Quaternion.LookRotation(Vector3.down);
    private Quaternion rotOffset;
    public PlayerController pc;
    public float shadowStrength;
    private Light light;
    public float maxXAngle = 50f;
    private float originalXAngle;
    public float maxSpotAngle = 50f;
    private float originalSpotAngle;
    private float originalIntensity;
    public float minIntensity = 0.01f;

    // Start is called before the first frame update
    void Start()
    {
        originalXAngle = transform.localEulerAngles.x;
        light = GetComponent<Light>();
        originalSpotAngle = light.spotAngle;
        originalIntensity = light.intensity;

        rotTarget = Quaternion.LookRotation(Vector3.down);
        rotOffset = Quaternion.RotateTowards(rotTarget, transform.rotation, 360);
    }

    // Update is called once per frame
    void Update()
    {
        float fuel;
        fuel = Mathf.Clamp(pc.fuel, 0f, 3f);
        light.intensity = originalIntensity * Mathf.Clamp(fuel / 3f,minIntensity,1f);
        //transform.rotation = t
        float jumpT = Mathf.Clamp01(pc.jumpT);
        //shadowStrength = 1f - jumpT;
        //light.shadowStrength = shadowStrength;
        Vector3 euler = transform.localEulerAngles;
        euler.x = originalXAngle + pc.jumpAccelerationCurve.Evaluate(pc.jumpT) * maxXAngle + 10f;
        if(!pc.isJumping)
        {
            euler.x = originalXAngle;
            light.spotAngle = originalSpotAngle;
        }
        transform.localEulerAngles = euler;
        light.spotAngle = originalSpotAngle - pc.jumpAccelerationCurve.Evaluate(pc.jumpT) * maxSpotAngle;
    }
}
