using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class backgroundCameraMovement : MonoBehaviour
{
    // private Vector2 middle
    public float panSensitivity = 0.003f;
    public float lookSensitivity = 0.003f;
    public float autoSpeed = 1f;
    public float autoRadius = 200f;
    private Quaternion originalRotation;
    private Vector3 originalPosition;
    private Vector2 autoOffset;
    private float t;

    // Start is called before the first frame update
    void Start()
    {
        t = 0f;
        originalRotation = transform.rotation;
        originalPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime * autoSpeed;
        autoOffset.x = Mathf.Cos(t) * autoRadius;
        autoOffset.y = Mathf.Sin(t) * autoRadius;
        Vector2 mousePos = Input.mousePosition;
        // IMPLEMENT MOUSEPOS CLAMP HERE
        Vector2 offset;
        offset.x = mousePos.x- Screen.width / 2;
        offset.y = mousePos.y- Screen.height / 2;
        offset += autoOffset;
        Vector3 rotation = new Vector3();
        rotation.y = offset.x;
        rotation.x = offset.y*-1;
        Vector3 position = new Vector3();
        position.y = offset.y;
        position.x = offset.x;
        position = transform.rotation * position ;

        transform.rotation = originalRotation * Quaternion.Euler(rotation * lookSensitivity);
        transform.position = originalPosition + position * panSensitivity;

    }

    void updateMiddle()
    {

    }
}
