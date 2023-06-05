using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditor.Experimental;
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
    public float originalFarThreshold;
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
    public float initialAngle;
    private bool angleTooMuch = false;
    private bool swapf = false;



    // Start is called before the first frame update
    void Start()
    {
        initialAngle = Vector3.Angle(leftFoot.forward, rightFoot.forward) -10f;
        //originalLegsDotProduct = Vector3.Dot(leftFoot.forward, rightFoot.forward);
        groundY = leftFoot.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        Time.timeScale = times;
        
        legsCOM.position = Vector3.Lerp(leftTip.position, rightTip.position, 0.5f);
        Vector3 temp = transform.position + transform.forward * bodyCOMf;
        temp.y = legsCOM.position.y;
        bodyCOM.position = temp;

        legSpeed = 6f + (pc.moveSpeed / 3f) * pc.acceleration * 2f ;
        farThreshold = 1f + originalFarThreshold * (pc.acceleration ) * 2f;

        updateLegPriority();
        Debug.Log("far foot (" + farthestFoot.name + ") distance = " + AH.xzDistance(farthestFoot.position, bodyCOM.position));
        COMd = AH.xzDistance(legsCOM.position,bodyCOM.position);

        checkCOM();
        moveLegs();
        // d = 1.5
        // 45degree = 1 , far = 2.09
        // 90 degree = 1.5 , far = 2.5
        // 180 degree = 2.4
    }

    private void updateLegPriority()
    {
        if (AH.xzDistance(leftTip.position, bodyCOM.position) < AH.xzDistance(rightTip.position, bodyCOM.position))
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
                 
                if (movingFoot == null)
                {
                    movingFoot = farthestFoot;
                }
                moveT = 0f;
                if (movingFoot == rightFoot && pc.acceleration < 0.1f)
                {
                    Debug.Log("change moving to right");
                    if (COMd > farThreshold) moveT = 0.0001f;
                        movingFoot = leftFoot;
                    footTargetPos = bodyLeftFoot.position;
                    footTargetRot = bodyLeftFoot.rotation;
                }
                else if (movingFoot == leftFoot && pc.acceleration < 0.1f)
                {
                    Debug.Log("change moving to left");
                    if (COMd > farThreshold) moveT = 0.0001f;
                    movingFoot = rightFoot;
                    footTargetPos = bodyRightFoot.position;
                    footTargetRot = bodyRightFoot.rotation;
                }
 
                
            }
            else
            {
                if (movingFoot == null)
                {
                    moveT= 1f;
                    return;
                }
                //TODO if left foot and accel set target to left foot body
                //if (pc.acceleration == 0f && (footTargetPos != bodyLeftFoot.position && footTargetPos != bodyRightFoot.position)){footTargetPos = movingFoot == leftFoot ? bodyLeftFoot.position : bodyRightFoot.position; moveT= 0f;}
                
                moveT += Time.deltaTime * legSpeed / (AH.xzDistance(movingFoot.position, footTargetPos) + Mathf.Epsilon);
                moveT = Mathf.Clamp01(moveT);
                Debug.Log("moving foot = " + movingFoot.name + " moveT = " + moveT);
                Vector3 pos = Vector3.Lerp(movingFoot.position, footTargetPos, moveT);
                pos.y = groundY+footLiftCurve.Evaluate(moveT) * footLift * Mathf.Clamp((AH.xzDistance(movingFoot.position, footTargetPos) + 0.001f),0f,footLift);
                Debug.Log("pos y = " + pos.y);
                Quaternion rot = Quaternion.Lerp(movingFoot.rotation, footTargetRot, moveT);
                movingFoot.position = pos;
                movingFoot.rotation = rot;
                if (!angleTooMuch && Vector3.Angle(leftFoot.forward, rightFoot.forward) < initialAngle)
                {
                    //Debug.Log("angle too low");
                    angleTooMuch = true;
                    moveT = 0f;
                    swapf = true;
                }
            }
        }
    }
    private void checkCOM()
    {
        if (moveT <= 0f)
        {
            //Debug.Log("angle foot down");
            //else 
            if (COMd > farThreshold)
            {
            if (Vector3.Angle(leftFoot.forward, rightFoot.forward) >= initialAngle)
            {
                angleTooMuch = false;
                //Debug.Log("angle normal");
            }
                Vector3 compar = farthestFoot.position;
                if (angleTooMuch)
                {
                    if(swapf)
                    compar =  closestFoot.position;
                    swapf = false;
                }
                Debug.Log("movingfoot = somefoot");
                if (AH.xzDistance(compar, bodyCOM.position) > farThreshold)
                {
                    //move farthest foot
                    movingFoot = farthestFoot; //isFirstLegMovement = true;
                    footTargetPos = farthestTarget.position;
                    footTargetRot = farthestTarget.rotation;
                    footTargetPos += transform.forward * AH.xzDistance(footTargetPos, movingFoot.position) * pc.acceleration * Mathf.Clamp01(Vector3.Dot(movingFoot.forward, farthestTarget.forward)) * 0.9f;
                }
                else
                {
                    //move closest foot
                    movingFoot = closestFoot; //isFirstLegMovement = true;
                    footTargetPos = closestTarget.position;
                    footTargetRot = closestTarget.rotation;
                    footTargetPos += transform.forward * AH.xzDistance(footTargetPos, movingFoot.position) * pc.acceleration * Mathf.Clamp01(Vector3.Dot(movingFoot.forward, closestTarget.forward)) * 0.9f;
                }
                //overshoot
            }
            else
            {
                Debug.Log("movingfoot = null");
                movingFoot = null;
            }
        }
    }

    private void OnDrawGizmos()
    {
        center = Vector3.Lerp(rightFoot.position,leftFoot.position, 0.5f);
        center.y = 1f;

        //UnityEditor.Handles.color = Color.white;
        //UnityEditor.Handles.DrawWireDisc(center, transform.up, footCircleRadius,thickness);

        Vector3 endPoint;
        //endPoint = lookAt.forward * footCircleRadius + center;
        //UnityEditor.Handles.DrawBezier(center, endPoint,endPoint,center, Color.white,null,thickness);
        //endPoint = rightFoot.forward* footCircleRadius + center;
        //UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.red, null, thickness);
        //endPoint = leftFoot.forward * footCircleRadius + center;
        //UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.blue, null, thickness);
        //endPoint = Quaternion.Euler(0f,footRotateThresholdAngle,0f) * leftFoot.forward * footCircleRadius + center;
        //UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.white, null, thickness);
        //endPoint = Quaternion.Euler(0f, -footRotateThresholdAngle, 0f) * rightFoot.forward * footCircleRadius + center;
        //UnityEditor.Handles.DrawBezier(center, endPoint, endPoint, center, Color.white, null, thickness);

        Vector3 startPoint = legsCOM.position;
        endPoint = bodyCOM.position;
        endPoint = startPoint + (endPoint - startPoint).normalized * farThreshold;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.cyan, null, 5f);
        endPoint = startPoint + (endPoint - startPoint).normalized * 0.3f;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.black, null, 5f);

    }
}
