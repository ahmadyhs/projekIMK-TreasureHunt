using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RigMass : MonoBehaviour
{
    public RigController controller;
    public float weight = 1f;
    // Start is called before the first frame update
    void Start()
    {
        controller.masses.Add(this);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
