using System.Collections;
using System.Collections.Generic;
using System.Net.NetworkInformation;
using UnityEditor.Rendering;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

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
    public static Vector3 changeY(Vector3 target, float y)
    {
        Vector3 temp = target;
        temp.y = y;
        return temp;
    }
    public static Vector3 offsetY(Vector3 target, float y)
    {
        Vector3 temp = target;
        temp.y += y;
        return temp;
    }

    public static float xzDistance(Vector3 a, Vector3 b)
    {
        float num = a.x - b.x;
        float num2 = a.z - b.z;
        return (float)Mathf.Sqrt(num * num + num2 * num2);
    }
    public static float getAcPythagoras(Vector3 a, Vector3 b, Vector3 c)
    {
        return Mathf.Sqrt((Vector3.Distance(b, c) * Vector3.Distance(b, c)) - (Vector3.Distance(a, b) * Vector3.Distance(a, b)));
    }
    public static float Angle(Vector3 vectorA, Vector3 vectorB) // a is 0 degree b on opposite side is 180-360 degree
    {
        vectorA.y = 0f;
        vectorB.y = 0f;
        float angle = Vector3.Angle(vectorA, vectorB);


        // Get the signed angle to determine the direction
        Vector3 crossProduct = Vector3.Cross(vectorA, vectorB);
        //Debug.Log("cross = "+ crossProduct);
        if (crossProduct.y < 0)
        {
            angle = 360f - angle;
        }

        return angle;
    }

    public static float GetCurveDerivative(AnimationCurve curve, float time)
    {
        float value = curve.Evaluate(time);

        // Find the two neighboring keyframes
        Keyframe[] keys = curve.keys;
        int prevIndex = 0;
        int nextIndex = keys.Length - 1;

        for (int i = 1; i < keys.Length; i++)
        {
            if (keys[i].time > time)
            {
                nextIndex = i;
                prevIndex = i - 1;
                break;
            }
        }

        // Calculate the derivative using neighboring keyframes
        float deltaTime = keys[nextIndex].time - keys[prevIndex].time;
        float deltaValue = keys[nextIndex].value - keys[prevIndex].value;
        float derivative = deltaValue / deltaTime;

        return derivative;
    }

    public static Quaternion RotateBetweenAngles(Vector3 currentDirection, Vector3 targetDirection, float minAngle, float maxAngle)
    {
        Quaternion currentRotation = Quaternion.LookRotation(currentDirection);
        Quaternion targetRotation = Quaternion.LookRotation(targetDirection);

        // Get the angle between the current and target rotations
        float angle = Quaternion.Angle(currentRotation, targetRotation);

        // Clamp the angle between the min and max angles
        float clampedAngle = Mathf.Clamp(angle, minAngle, maxAngle);

        // Interpolate towards the target rotation with the clamped angle
        return Quaternion.RotateTowards(currentRotation, targetRotation, clampedAngle);
    }

    public static Vector3 ClampPointToLine(Vector3 point, Vector3 linePoint, Vector3 startPoint, Vector3 normalPoint)
    {
        Vector3 lineDirection = (normalPoint - linePoint).normalized;
        Vector3 pointVector = point - linePoint;
        float dotProduct = Vector3.Dot(pointVector, lineDirection);

        if (dotProduct < 0)
        {
            // Point is on the same side as the target point
            lineDirection = Vector3.Cross(lineDirection, Vector3.up).normalized;
            Vector3 intersectionPoint;
            NormalVectorIntersection(startPoint, (point - startPoint).normalized, linePoint, lineDirection, out intersectionPoint);
            //Vector3 projectedVector = Vector3.Project(point-normalPoint, lineDirection);
            //Vector3 clampedPoint = linePoint + projectedVector;
            return intersectionPoint;
        }
        else
        {
            // Point is on the opposite side of the target point
            return point;
        }
    }
    public static bool NormalVectorIntersection(Vector3 point1, Vector3 normal1, Vector3 point2, Vector3 normal2, out Vector3 intersectionPoint)
    {
        Vector3 lineVector = point2 - point1;
        Vector3 cross = Vector3.Cross(normal1, normal2);

        // Check if the vectors are parallel or collinear
        if (cross.sqrMagnitude < Mathf.Epsilon)
        {
            intersectionPoint = Vector3.zero;
            return false;
        }

        float denominator = Vector3.Dot(cross, cross);
        float scale1 = Vector3.Dot(Vector3.Cross(lineVector, normal2), cross) / denominator;

        intersectionPoint = point1 + normal1 * scale1;
        return true;
    }

    public static Vector3 GetPerpendicularVector(Vector3 vector)
    {
        Vector3 referenceVector = Vector3.up; // An arbitrary reference vector
        return Vector3.Cross(vector, referenceVector);
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

