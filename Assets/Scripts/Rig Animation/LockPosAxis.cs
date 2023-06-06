using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class LockPosAxis : MonoBehaviour
{
    public Vector3 lockVector;
    private Vector3 originalPos;
    // Start is called before the first frame update
    void Start()
    {
        originalPos = transform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = AH.lerp(transform.position, originalPos,lockVector);
    }
}
