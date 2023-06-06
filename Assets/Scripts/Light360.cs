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

    // Start is called before the first frame update
    void Start()
    {

        originalXAngle = transform.localEulerAngles.x;
        light = GetComponent<Light>();
        originalSpotAngle = light.spotAngle;

        rotTarget = Quaternion.LookRotation(Vector3.down);
        rotOffset = Quaternion.RotateTowards(rotTarget, transform.rotation, 360);
    }

    // Update is called once per frame
    void Update()
    {

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
