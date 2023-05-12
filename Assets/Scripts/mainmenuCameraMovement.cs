using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using UnityEngine;

public class mainmenuCameraMovement : MonoBehaviour
{
    // private Vector2 middle
    public float sensitivity = 0.001f;
    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 mousePos = Input.mousePosition;
        // IMPLEMENT MOUSEPOS CLAMP HERE
        Vector2 offset;
        offset.x = mousePos.x- Screen.width / 2;
        offset.y = mousePos.y- Screen.height / 2;
        Vector3 rotation = new Vector3();
        rotation.y = offset.x;
        rotation.x = offset.y*-1;
        transform.rotation = originalRotation * Quaternion.Euler(rotation * sensitivity);

    }

    void updateMiddle()
    {

    }
}
