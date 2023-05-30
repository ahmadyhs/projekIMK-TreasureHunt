using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using AH = AdnanHelper;

public class tesPlayer : MonoBehaviour
{
    public Transform leftFoot;
    public Transform rightFoot;
    public Transform bodyLeftFoot;
    public Transform bodyRightFoot;
    public Transform leftTip;
    public Transform rightTip;
    public Transform lookAt;
    public Transform head;
    public PlayerController pc;
    public Transform bodyCOM;
    public Transform legsCOM;
    public float farThreshold;
    public float bodyCOMf = 0.6f;
    public float legsCOMd = 0.6f;
    public float COMd;
    private Vector3 center;
    public float legSpeed = 5f;
    public float footLift = 2f;
    public float footCircleRadius = 5f;
    public float thickness = 1f;
    public float footRotateThresholdAngle;
    private float originalLegsDotProduct = 100f;
    private Transform closestFoot;
    private Transform farthestFoot;
    private Transform movingFoot;
    private float moveT = 0f;
    public AnimationCurve footLiftCurve;
    private bool isFirstLegMovement= true;
    public float times = 1f;
    private Vector3 footTargetPos;
    private Quaternion footTargetRot;
    private Transform farthestTarget;
    private Transform closestTarget;
    private float groundY;

    // Start is called before the first frame update
    void Start()
    {
        //originalLegsDotProduct = Vector3.Dot(leftFoot.forward, rightFoot.forward);
        groundY = leftFoot.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = times;
        
        legsCOM.position = Vector3.Lerp(leftTip.position, rightTip.position, 0.5f);
        Vector3 temp = transform.forward * bodyCOMf;
        temp.y = legsCOM.position.y;
        bodyCOM.position = temp;

        updateLegPriority();
        Debug.Log("far foot (" + farthestFoot.name + ") distance = " + Vector2.Distance(farthestFoot.position, bodyCOM.position));
        COMd = Vector2.Distance(legsCOM.position,bodyCOM.position);

        checkCOM();
        moveLegs();
        // d = 1.5
        // 45degree = 1 , far = 2.09
        // 90 degree = 1.5 , far = 2.5
    }

    private void updateLegPriority()
    {
        if (Vector2.Distance(leftTip.position, bodyCOM.position) < Vector2.Distance(rightTip.position, bodyCOM.position))
        {
            closestFoot = leftFoot;
            closestTarget = bodyLeftFoot;
            farthestFoot = rightFoot;
            farthestTarget = bodyRightFoot;
        }
        else
        {
            closestFoot = rightFoot;
            closestTarget = bodyRightFoot;
            farthestFoot = leftFoot;
            farthestTarget = bodyLeftFoot;
        }
    }

    private void moveLegs()
    {
        if(true)
        {
            Debug.Log("move t = " + moveT);
            if (moveT >= 1f)
            {
                moveT = 0f;
                if (movingFoot == rightFoot)
                {
                    Debug.Log("change moving to right");
                    if (leftFoot.rotation != bodyLeftFoot.rotation) moveT = 0.0001f;
                        movingFoot = leftFoot;
                    footTargetPos = bodyLeftFoot.position;
                    footTargetRot = bodyLeftFoot.rotation;
                }
                else if (movingFoot == leftFoot)
                {
                    Debug.Log("change moving to left");
                    if (rightFoot.rotation != bodyRightFoot.rotation) moveT = 0.0001f;
                    movingFoot = rightFoot;
                    footTargetPos = bodyRightFoot.position;
                    footTargetRot = bodyRightFoot.rotation;
                }
 
                
            }
            else
            {
                moveT += Time.deltaTime * legSpeed / (Vector2.Distance(movingFoot.position, footTargetPos) + Mathf.Epsilon);
                moveT = Mathf.Clamp01(moveT);
                Debug.Log("moving foot = " + movingFoot.name + " moveT = " + moveT);
                Vector3 pos = Vector3.Lerp(movingFoot.position, footTargetPos, moveT);
                pos.y = groundY+footLiftCurve.Evaluate(moveT) * footLift;
                Quaternion rot = Quaternion.Lerp(movingFoot.rotation, footTargetRot, moveT);
                movingFoot.position = pos;
                movingFoot.rotation = rot;
            }
        }
    }
    private void checkCOM()
    {
        if (moveT == 0f)
            //    if (rightFoot.rotation != bodyRightFoot.rotation)
            //{
            //    movingFoot = rightFoot; //isFirstLegMovement = true;
            //    footTargetPos = bodyRightFoot.position;
            //    footTargetRot = bodyRightFoot.rotation;
            //}
            //else if(leftFoot.rotation != bodyLeftFoot.rotation || rightFoot.rotation != bodyRightFoot.rotation)
            //{
            //    movingFoot = leftFoot; //isFirstLegMovement = true;
            //    footTargetPos = bodyLeftFoot.position;
            //    footTargetRot = bodyLeftFoot.rotation;
            //}
            //else 
            if (COMd > farThreshold)
        {
            if(Vector2.Distance(farthestFoot.position,bodyCOM.position) > farThreshold)
            {
                //move farthest foot
                movingFoot = farthestFoot; //isFirstLegMovement = true;
                footTargetPos = farthestTarget.position;
                footTargetRot = farthestTarget.rotation;
                
            }
            else
            {
                //move closest foot
                movingFoot = closestFoot; //isFirstLegMovement = true;
                footTargetPos = closestTarget.position;
                footTargetRot = closestTarget.rotation;
            }
        }
            else if (leftFoot.rotation == bodyLeftFoot.rotation || rightFoot.rotation == bodyRightFoot.rotation)
            {
                movingFoot = null;
            }
    }

    private void OnDrawGizmos()
    {
        center = Vector3.Lerp(rightFoot.position,leftFoot.position, 0.5f);
        center.y = 1f;

        UnityEditor.Handles.color = Color.white;
        UnityEditor.Handles.DrawWireDisc(center, transform.up, footCircleRadius,thickness);

        Vector3 endPoint;
        endPoint = lookAt.forward * footCircleRadius + center;
        UnityEditor.Handles.DrawBezier(center, endPoint,endPoint,center, Color.white,null,thickness);
        endPoint = rightFoot.forward* footCircleRadius + center;
        UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.red, null, thickness);
        endPoint = leftFoot.forward * footCircleRadius + center;
        UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.blue, null, thickness);
        endPoint = Quaternion.Euler(0f,footRotateThresholdAngle,0f) * leftFoot.forward * footCircleRadius + center;
        UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.white, null, thickness);
        endPoint = Quaternion.Euler(0f, -footRotateThresholdAngle, 0f) * rightFoot.forward * footCircleRadius + center;
        UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.white, null, thickness);

    }
}
