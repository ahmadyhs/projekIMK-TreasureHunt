using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //public float rotation;
    public float moveSpeed;
    public float speedBoostModifier;
    public float speedBoostRotationModifer;
    public float jumpspeed = 1f;
    public bool jumpBoost = false;
    public bool speedBoost = false;
    public float rotationSpeed=1f;
    public float rotationTarget=0f;
    float rotateDir = 0;
    float rotateState = 0;
    public int rotationPressed = 0;
    public KeyCode north = KeyCode.W;
    public KeyCode east = KeyCode.D;
    public KeyCode south = KeyCode.S;
    public KeyCode west = KeyCode.A;
    public KeyCode jumpkey = KeyCode.Space;
    float movementT;
    float stopT;
    float jumpT;
    bool isDirectionalKeyPressed = false;
    public AnimationCurve accelerationCurve;
    public AnimationCurve deaccelerationCurve;
    public AnimationCurve jumpAccelerationCurve;
    public AnimationCurve jumpBoostAccelerationCurve;
    bool isJumping = false;

    // Start is called before the first frame update
    void Start()
    {
        movementT = 0f;
        jumpT = 0.38f;
        stopT = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(south) || Input.GetKey(west) || Input.GetKey(north) || Input.GetKey(east)) isDirectionalKeyPressed = true;
        else isDirectionalKeyPressed = false;
        updatePosition();
        updateRotation();
        checkJump();
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
            rotationTarget += 180;
            rotationPressed += 1;
        }
        if (Input.GetKey(west))
        {
            rotationTarget += 270;
            rotationPressed += 1;
        }
        if (Input.GetKey(east))
        {
            rotationTarget += 90;
            rotationPressed += 1;
        }
        if (Input.GetKey(north))
        {
            rotationTarget += get0or360(rotationTarget / rotationPressed);
            rotationPressed += 1;
        }
        if (rotationPressed == 0)rotationPressed= 1;
        if (isDirectionalKeyPressed) rotationTarget = rotationTarget / rotationPressed;
        //Debug.Log(rotationTarget);
        if (rotationTarget == 0 || rotationTarget == 360) rotationTarget = get0or360(rotation);
        rotateDir = Mathf.Sign(rotationTarget - rotation);
        rotateState = rotateDir;
        if (Mathf.Abs( rotationTarget - rotation)> 180) rotateDir *= -1;
    }

    private void OnCollisionEnter(Collision other)
    {
        Debug.Log("touch something");
        if (other.gameObject.tag == "ground")
        {
            Debug.Log("touch ground");
            isJumping = false;
            transform.position = new Vector3(transform.position.x, other.transform.position.y,transform.position.z);
        }

    }
    private void OnCollisionExit(Collision other)
    {
        Debug.Log("exit something");
        if (other.gameObject.tag == "ground")
        {
            Debug.Log("exit ground");
            isJumping = true;
        }
    }

    private void checkJump()
    {
        if (!isJumping)
        {
            if (Input.GetKey(jumpkey))
            {
                isJumping= true;
                jumpT = 0f;
            }
        }
    }
    private void checkMove()
    {
        if (isDirectionalKeyPressed)
        {

        }
    }
    private void updatePosition()
    {
        if (isJumping) 
        {
            movementT = 1f;
            AnimationCurve curve;
            if (jumpBoost) curve = jumpBoostAccelerationCurve;
            else curve = jumpAccelerationCurve;
            transform.position = transform.position + transform.up * curve.Evaluate(jumpT) * Time.deltaTime * jumpspeed;
        } 
        //else jjumpspeed= 0f;
        jumpT += Time.deltaTime;
        if (isDirectionalKeyPressed)
        {
            stopT = 0f;
            movementT += Time.deltaTime;
        }
        else if(speedBoost && !isJumping) stopT += Time.deltaTime;
        else movementT = 0f;
        float speed = moveSpeed;
        if (speedBoost) speed *= speedBoostModifier;
        transform.position = transform.position + transform.forward * accelerationCurve.Evaluate(movementT) * speed * Time.deltaTime * deaccelerationCurve.Evaluate(stopT);
    }
    private void updateRotation()
    {
        float rotation = transform.eulerAngles.y;
        if (speedBoost)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, rotationTarget, transform.eulerAngles.z);
        }
        else if(rotation != rotationTarget)
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
