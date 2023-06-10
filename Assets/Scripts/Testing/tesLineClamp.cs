using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using UnityEngine;
using AH = AdnanHelper;

public class tesLineClamp : MonoBehaviour
{
    public Transform target;
    public Transform linePoint;
    public Transform normalPoint;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }
 #if (UNITY_EDITOR)

    private void OnDrawGizmos()
    {
        Vector3 startPoint = transform.position;
        Vector3 endPoint = AH.ClampPointToLine(target.position, linePoint.position,transform.position, normalPoint.position);
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.cyan, null, 5f);
        startPoint = normalPoint.position;
        endPoint = linePoint.position;
        UnityEditor.Handles.DrawBezier(startPoint, endPoint, endPoint, startPoint, Color.red, null, 5f);
    }
#endif
}
