using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float rotation;
    public float moveSpeed;
    public float rotationSpeed=1f;
    public float rotationTarget=0f;
    float rotateDir = 0;
    float rotateState = 0;
    public int rotationPressed = 0;
    public KeyCode north = KeyCode.W;
    public KeyCode east = KeyCode.D;
    public KeyCode south = KeyCode.S;
    public KeyCode west = KeyCode.A;
    float movementT;
    bool isDirectionalKeyPressed = false;
    public AnimationCurve accelerationCurve;
    
    // Start is called before the first frame update
    void Start()
    {
        movementT = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(south) || Input.GetKey(west) || Input.GetKey(north) || Input.GetKey(east)) isDirectionalKeyPressed = true;
        else isDirectionalKeyPressed = false;
        updatePosition();
        updateRotation();
    }
    private void FixedUpdate()
    {
        updateRotationTarget();
    }

    private void updateRotationTarget()
    {
        float rotation = transform.eulerAngles.y;
        if (isDirectionalKeyPressed)
        {
            rotationPressed = 0;
            rotationTarget = 0f;
        }
        if (Input.GetKey(south))
        {
            rotationTarget += 90;
            rotationPressed += 1;
        }
        if (Input.GetKey(west))
        {
            rotationTarget += 180;
            rotationPressed += 1;
        }
        if (Input.GetKey(north)) {
            rotationTarget += 270;
            rotationPressed += 1;
        }
        if (Input.GetKey(east))
        {
            rotationTarget += get0or360(rotationTarget/rotationPressed);
            rotationPressed += 1;
        }
        if (rotationPressed == 0)rotationPressed= 1;
        if (isDirectionalKeyPressed) rotationTarget = rotationTarget / rotationPressed;
        Debug.Log(rotationTarget);
        if (rotationTarget == 0 || rotationTarget == 360) rotationTarget = get0or360(rotation);
        rotateDir = Mathf.Sign(rotationTarget - rotation);
        rotateState = rotateDir;
        if (Mathf.Abs( rotationTarget - rotation)> 180) rotateDir *= -1;
    }

    private void updatePosition()
    {
        if (isDirectionalKeyPressed) movementT += Time.deltaTime;
        else movementT = 0f;
        transform.position = transform.position + transform.forward * accelerationCurve.Evaluate(movementT) * Time.deltaTime * moveSpeed;
    }
    private void updateRotation()
    {
        float rotation = transform.eulerAngles.y;
        if(rotation != rotationTarget)
        {
            rotation = rotation + rotateDir * rotationSpeed * Time.deltaTime;
            if (rotation < 0 && rotateDir < 0) { rotation += 360; rotateState *= -1; };
            if (rotation > 360 && rotateDir > 0) { rotation -= 360; rotateState *= -1; };
            if (Mathf.Sign(rotationTarget - rotation) != rotateState)  rotation = rotationTarget;
            transform.eulerAngles = new Vector3(transform.eulerAngles.x,rotation, transform.eulerAngles.z);

        }
    }
    private float get0or360(float target)
    {
        if (target > 180) return 360;
        else return 0;
    }
}
