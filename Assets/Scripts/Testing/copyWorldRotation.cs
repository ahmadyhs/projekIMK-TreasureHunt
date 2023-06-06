using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class copyWorldRotation : MonoBehaviour
{
    public Transform target;
    public Vector3 modifier;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void copy()
    {
        transform.rotation = Quaternion.Euler(AH.lerp(transform.rotation.eulerAngles,target.rotation.eulerAngles,modifier));
    }
}
