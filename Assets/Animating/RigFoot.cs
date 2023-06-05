using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class RigFoot : MonoBehaviour
{
    public RigFoot otherFoot;
    public Transform thigh;
    public Quaternion thighInitialRot;
    public Transform tip;
    public Transform target;
    public Vector3 startPos;
    public Quaternion startRot;
    public Vector3 targetPos;
    public Quaternion targetRot;
    public Vector3 position { get { return transform.position; } set { transform.position = value; } }
    public Quaternion rotation { get { return transform.rotation; } set { transform.rotation = value; } }
    public Vector3 forward { get { return transform.forward; } private set { } }
    // Start is called before the first frame update
    void Start()
    {
        thighInitialRot = thigh.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void updateTarget()
    {
        this.startPos = transform.position;
        this.startRot = transform.rotation;
        this.targetPos = target.position;
        //this.targetPos = AH.ClampPointToLine(target.position, otherFoot.position + getOtherFootDir()*2f, transform.position,transform.position+ getOtherFootDir());
        this.targetRot = target.rotation;
    }
    public Vector3 getOtherFootDir()
    {
        //if (Vector3.Cross(transform.forward, (otherFoot.position - transform.position).normalized).y < 0)
        if(name == "rightFoot")
        {
            return Vector3.left;
        }
        else return Vector3.right;
    }
}
