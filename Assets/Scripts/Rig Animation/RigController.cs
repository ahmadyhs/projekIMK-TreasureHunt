using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental;
using UnityEngine;
using AH = AdnanHelper;

public class RigController: MonoBehaviour
{

    public RigFoot leftFoot;
    public RigFoot rightFoot;
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
    private RigFoot closestFoot;
    private RigFoot farthestFoot;
    private RigFoot movingFoot;
    private float moveT = 0f;
    public AnimationCurve footLiftCurve;
    private bool isFirstLegMovement= true;
    public float times = 1f;
    private Transform farthestTarget;
    private Transform closestTarget;
    private float groundY;
    public float initialAngle;
    private bool angleTooMuch = false;
    private bool swapf = false;
    public List<RigMass> masses;
    public float footAngle;
    public float maxFootAngle = 130f;
    public float originalLegSpeed = 3f;
    private Vector3 frowad;
    private bool drawit = false;
    private float groundYoffset;

    // Start is called before the first frame update
    void Start()
    {
        groundYoffset = leftFoot.target.position.y - pc.groundY;
        initialAngle = (AH.Angle(leftFoot.forward, rightFoot.forward)-2f);
        //originalLegsDotProduct = Vector3.Dot(leftFoot.forward, rightFoot.forward);
    }
    int checkAngle()
    {
        if(checkAngleToohHigh()) return 1;
        //if (checkAngleToohLow()) return -1;
        else return 0;
    }
    bool checkAngleToohHigh()
    {
        footAngle = AH.Angle(leftFoot.forward, rightFoot.forward);
        if (footAngle > maxFootAngle)
        {
            drawit =  true;
            Vector3 crossProduct = Vector3.Cross(leftFoot.forward, rightFoot.forward);
            ////Debug.Log("cross = " + crossProduct);
            //Debug.Log(movingFoot.name +" angle too high = " + leftFoot.name + leftFoot.forward +rightFoot.name + rightFoot.forward.ToString() + " cross = " + crossProduct);
            return (true);

        }
        return false;
    }
    bool checkAngleToohLow()
    {
        footAngle = AH.Angle(leftFoot.forward, rightFoot.forward);
        return (footAngle < initialAngle+1f);
    }
    void calculateCOM()
    {
        Vector3 temp  = new Vector3();
        float totalWeigth = 0f;
        foreach (RigMass mass in masses) 
        {
            temp += mass.transform.position * mass.weight;
            totalWeigth += mass.weight;
        }
        temp /= totalWeigth;
        bodyCOM.position = AH.lerp(bodyCOM.position, temp, 1f, 0f, 1f);

        temp = AH.lerp(rightFoot.tip.position, leftFoot.tip.position, 0.5f, 0f, 0.5f);
        legsCOM.position = temp;
    }

    // Update is called once per frame
    void Update()
    {
        groundY = pc.groundY + groundYoffset;
        //footAngle = AH.Angle(leftFoot.forward, rightFoot.forward);
        Time.timeScale = times;

        legSpeed = originalLegSpeed * pc.moveSpeed / 10f;
        //legSpeed = originalLegSpeed ;
        farThreshold = 1f + originalFarThreshold * (pc.acceleration ) * 2f;
        ////Debug.Log("anglOK " + checkAngle());
        calculateCOM();
        updateLegPriority();
        ////Debug.Log("far foot (" + farthestFoot.name + ") distance = " + AH.xzDistance(farthestFoot.position, bodyCOM.position));
        COMd = AH.xzDistance(legsCOM.position,bodyCOM.position);

        checkCOM();
        moveLegs();
    }

    private void updateLegPriority()
    {
        if (AH.xzDistance(leftFoot.tip.position, bodyCOM.position) < AH.xzDistance(rightFoot.tip.position, bodyCOM.position))
        {
            closestFoot = leftFoot;
            farthestFoot = rightFoot;
        }
        else
        {
            closestFoot = rightFoot;
            farthestFoot = leftFoot;
        }
    }

    private void moveLegs()
    {
        //Debug.Log("move t = " + moveT);
        if (moveT >= 1f)
        {
            if (movingFoot == null)
            {
                movingFoot = farthestFoot;
            }
            moveT = 0f;
            if (pc.acceleration < 0.1f && pc.isRotated())
            {
                //Debug.Log("change moving to " + movingFoot.otherFoot.name);
                if (COMd > 0.3f) moveT = 0.0001f;
                movingFoot = movingFoot.otherFoot;
                movingFoot.updateTarget();
            }
        }
        else
        {
            if (movingFoot == null)
            {
                moveT= 1f;
                return;
            }
            moveT += Time.deltaTime * legSpeed * Mathf.Clamp((AH.xzDistance(movingFoot.startPos, movingFoot.target.position)*4f+Vector3.Angle(movingFoot.startRot * Vector3.forward,movingFoot.target.rotation * Vector3.forward)/180f) + 0.001f,0.1f,2f); ;
            moveT = Mathf.Clamp01(moveT);
            //TODO if left foot and accel set target to left foot body
            //if (pc.acceleration == 0f && (footTargetPos != bodyLeftFoot.position && footTargetPos != bodyRightFoot.position)){footTargetPos = movingFoot == leftFoot ? bodyLeftFoot.position : bodyRightFoot.position; moveT= 0f;}

            //Debug.Log("moving foot = " + movingFoot.name + " moveT = " + moveT);
            Vector3 pos = Vector3.Lerp(movingFoot.startPos, movingFoot.targetPos, moveT);
            pos.y = groundY + (footLiftCurve.Evaluate(moveT) * AH.xzDistance(movingFoot.startPos, movingFoot.targetPos) * footLift);
            //Debug.Log("pos y = " + pos.y);
            //pos.y = groundY;
            Quaternion rot = Quaternion.Lerp(movingFoot.startRot, movingFoot.targetRot, moveT);
            Quaternion originalRot = movingFoot.rotation;
            movingFoot.rotation = rot;
            //frowad = (rot * Vector3.forward);
            int angleStatus = checkAngle();
            if (angleStatus != 0)
            {
                movingFoot.updateTarget();
                //angleTooMuch = true;
                //legSpeed = originalLegSpeed * 4f;
                moveT = 0.00001f;
                swapf = true;
                pos = movingFoot.position;
                pos.y = groundY;
                rot = movingFoot.otherFoot.rotation;
                movingFoot.otherFoot.rotation = movingFoot.otherFoot.target.rotation;
                angleStatus = checkAngle();
                movingFoot.otherFoot.rotation = rot;

                movingFoot.rotation = originalRot;
                movingFoot.position = pos;
                movingFoot = movingFoot.otherFoot;
                movingFoot.updateTarget();
                if (angleStatus == 1)
                {
                    ////Debug.Log("angle too high = " + AH.Angle(rot * Vector3.forward, movingFoot.forward));
                    Quaternion rot2 = Quaternion.RotateTowards(movingFoot.rotation, movingFoot.otherFoot.rotation, 90f);
                    if (rot2== movingFoot.rotation)
                        rot2 = Quaternion.RotateTowards(movingFoot.rotation, movingFoot.otherFoot.targetRot, 90f);
                    //Debug.Log("this called" + movingFoot.otherFoot.name);
                    movingFoot.targetRot = rot2;
                    //movingFoot.targetPos = movingFoot.position;
                    //movingFoot.targetPos = Vector3.Lerp(movingFoot.position,movingFoot.target.position,0.5f);
                }
                //else Debug.Log("not called this");
                frowad = (movingFoot.targetRot * Vector3.forward);
                return;
            }
            //legSpeed = originalLegSpeed;
            frowad = (movingFoot.targetRot * Vector3.forward);
            movingFoot.position = pos;
        }
    }
    private void checkCOM()
    {

        if (moveT <= 0f)
        {
            //Debug.Log("anglefoot down");
            //else 
            //if (angleTooMuch && checkAngle())
            //    {
            //        angleTooMuch = false;
            ////        Debug.Log("angle normal" + AH.Angle(leftFoot.forward, rightFoot.forward));
            //    }
            //if (swapf)
            //{
            ////    Debug.Log("(angle too low)swapped " + movingFoot.name + " to " + movingFoot.otherFoot.name);
            //    movingFoot = movingFoot.otherFoot;
            //    movingFoot.updateTarget();
            //    swapf = false;
            //    return;
            //}
            if (COMd > farThreshold )
            {
                ////Debug.Log("movingfoot = somefoot");
                //if (AH.xzDistance(farthestFoot.tip.position, bodyCOM.position) > farThreshold)
                //{
                //    //move farthest foot
                //    movingFoot = farthestFoot; //isFirstLegMovement = true
                //}
                //else
                //{
                //    //move closest foot
                //    movingFoot = closestFoot; //isFirstLegMovement = true;
                //}
                movingFoot = farthestFoot;
                //overshoot
                movingFoot.updateTarget();
                movingFoot.targetPos += transform.forward * AH.xzDistance(movingFoot.targetPos, movingFoot.otherFoot.position) * pc.acceleration * Mathf.Clamp(Vector3.Dot(movingFoot.forward, movingFoot.target.forward),1f,1f) * 0.9f;
            }
            else
            {
                //Debug.Log("movingfoot = null");
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

        Vector3 startPoint = legsCOM.position;
        endPoint = bodyCOM.position;
        endPoint = startPoint + (endPoint - startPoint).normalized * farThreshold;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.cyan, null, 5f);
        endPoint = startPoint + (endPoint - startPoint).normalized * 0.3f;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.black, null, 5f);
        //if(drawit)
            dg();
    }
    void dg()
    {
        Vector3 endPoint;
        endPoint = bodyCOM.position;
        drawit= false;
        Vector3 startPoint;
        if (movingFoot != null)
            startPoint = movingFoot.position;
        else return;
        endPoint = startPoint + frowad.normalized * 10f;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.green, null, 10f);
        endPoint = startPoint + movingFoot.forward* 10f;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.white, null, 5f);
        //Debug.Log("the white is = " + movingFoot.name);
    }
}
