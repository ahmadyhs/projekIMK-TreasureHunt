using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using AH = AdnanHelper;

public class RigYController : MonoBehaviour
{
    public Transform target;

    public Transform target2;
    public Transform target3;
    public Transform anchor;
    public float weifh;
    public float offy;
    public float originalDistance;
    public RigController stand;
    // Start is called before the first frame update
    void Start()
    {
        offy = transform.localPosition.y + offy;
        //originalDistance = AH.getAcPythagoras(AH.lerp(anchor.position, target.position, 0f, 1f, 0f), target.position, anchor.position);
        originalDistance = Vector3.Distance(target.position, target2.position);
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = target.rotation;
        Vector3 a = Vector3.Lerp(target.position,target2.position,0.5f);
        Vector3 b = Vector3.zero;
        //b.y = (offy - Mathf.Clamp(originalDistance - AH.getAcPythagoras(AH.lerp(anchor.position, target.position, 0f, 1f, 0f), target.position, anchor.position),0f,Mathf.Infinity) )*weifh;
        b.y = offy - (Mathf.Clamp(Vector3.Distance(target.position, target2.position) - originalDistance, 0f, Mathf.Infinity))*weifh;
        transform.localPosition = AH.lerp(transform.localPosition, b, 0f, 1f, 0f);
         a = Vector3.Lerp(a, target3.position, 0.5f);
        a = AH.lerp(transform.position, a, 1f, 0f, 1f);
        if(!stand.pc.isJumping)
            transform.position = a;
        a = AH.lerp(anchor.position,target.position,0f,1f,0f);
        //Debug.Log("kakian = "+ (( AH.getAcPythagoras(a, target.position, anchor.position))));
    }
    private void OnDrawGizmos()
    {
        //Vector3 endPoint = Vector3.zero;
        //Vector3 startPoint = AH.lerp(anchor.position, target.position, 0f, 1f, 0f);
        //endPoint = target.position;
        //UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.white, null, 5f);
        //endPoint = anchor.position;
        //UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.white, null, 5f);
        //startPoint = target.position;
        //UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.white, null, 5f);

    }
}
