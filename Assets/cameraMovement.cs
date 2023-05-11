using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraMovement : MonoBehaviour
{
    public Vector3 offset;
    public Vector3 lookOffset;
    public Vector3 lookConstraint;
    public GameObject target;
    public float distance = 1f;
    public Vector3 targetPoint;
    public float moveSpeed = 50f;
    // Start is called before the first frame update
    void Start()
    {
        offset = offset + (transform.position - target.transform.position);
    }

    // Update is called once per frame
    void Update()
    {
        targetPoint = lookOffset + new Vector3(target.transform.position.x * lookConstraint.x// + transform.position.x * (1-lookConstraint.x)
            , target.transform.position.y * lookConstraint.y// + transform.position.y * (1 - lookConstraint.y)
            , target.transform.position.z * lookConstraint.z// + transform.position.z * (1 - lookConstraint.z) 
            );
        transform.position = Vector3.Lerp(transform.position, target.transform.position + offset * distance, Time.deltaTime * moveSpeed);
        //transform.LookAt(targetPoint);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(targetPoint - this.transform.position,Vector3.up),Time.deltaTime * moveSpeed*0.5f);
    }
}
