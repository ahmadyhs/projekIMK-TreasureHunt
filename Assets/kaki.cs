using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class kaki : MonoBehaviour
{
    public Transform target;
    public Transform target2;
    public Transform target3;
    public float weifh;
    public float offy;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //this.transform.rotation = target.rotation;
        Vector3 a = Vector3.Lerp(target.position,target2.position,0.5f);
         a = Vector3.Lerp(a, target3.position, 0.5f);
        a = AH.lerp(transform.position, a, 1f, 0f, 1f);
        a.y = (offy-Vector3.Distance(target.position, target2.position)*weifh);
        transform.position = a; 
    }
}
