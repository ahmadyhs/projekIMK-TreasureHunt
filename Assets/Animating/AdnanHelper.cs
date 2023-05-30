using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AdnanHelper
{
    public  static Vector3 MultiplyVector3(Vector3 vector, Vector3 multiplier)
    {
        vector.x *= multiplier.x;
        vector.y *= multiplier.y;
        vector.z *= multiplier.z;
        return vector;
    }
    public static Vector3 lerp(Vector3 a, Vector3 b, Vector3 value)
    {
        return lerp(a,b,value.x,value.y, value.z);
    }
    public static Vector3 lerp(Vector3 a, Vector3 b, float valueX, float valueY, float valueZ)
    {
        Vector3 result;
        result.x = Mathf.Lerp(a.x, b.x, valueX);
        result.y = Mathf.Lerp(a.y, b.y, valueY);
        result.z = Mathf.Lerp(a.z, b.z, valueZ);
        return result;
    }
    //public static Transform copy(Transform source)
    //{
    //    if(source == null) return null;
    //    Transform ret = Transform.Instantiate(;
    //    ret.localPosition = source.localPosition;
    //    ret.localRotation = source.localRotation;   
    //    ret.localScale = source.localScale;
    //    ret.position= source.position;
    //    ret.rotation= source.rotation;
    //    ret.name= source.name;
    //    ret.eulerAngles= source.eulerAngles;
    //    ret.localEulerAngles= source.localEulerAngles;
    //    return ret;
    //}
}

