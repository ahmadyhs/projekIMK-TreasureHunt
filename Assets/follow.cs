using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class follow : MonoBehaviour
{
    public Transform target;
    public Vector3 modifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = AH.lerp(transform.position, target.position, modifier);
    }

    private void OnDrawGizmos()
    {
        transform.position = AH.lerp(transform.position, target.position, modifier);
    }
}
