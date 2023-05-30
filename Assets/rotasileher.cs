using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class rotasileher : MonoBehaviour
{
    public Quaternion rotationt;
    public Transform reference;
    public float speed = 10f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        rotationt= transform.rotation;
    }
    void Update()
    {
    }
    private void LateUpdate()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles.x, reference.rotation.eulerAngles.y, transform.rotation.eulerAngles.z);
        //transform.rotation = rotationt;

    }
}
