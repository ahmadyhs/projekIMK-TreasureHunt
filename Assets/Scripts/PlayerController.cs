using System;
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
    public Quaternion rotationTarget;
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
    Vector3 direction;

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
        Debug.Log("Horizontal = " + Input.GetAxisRaw("Horizontal"));
        Debug.Log("Vertical = " + Input.GetAxis("Vertical"));
        Debug.Log("Jump = " + Input.GetAxis("Jump"));
        updateDirection();
        isDirectionalKeyPressed = direction != Vector3.zero;
        checkJump();
        updateRotationTarget();
        updateRotation();
        updatePosition();
    }
    private void FixedUpdate()
    {
    }

    private void updateRotationTarget()
    {
        if(isDirectionalKeyPressed)
        rotationTarget = Quaternion.LookRotation(direction, transform.up);
    }
    private void updateRotation()
    {
        float speed = Time.deltaTime * rotationSpeed;
        if (speedBoost) speed = 360f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, speed);
    }
    private void updateDirection()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
    }
    private void checkJump()
    {
        if (!isJumping)
        {
            if (Input.GetKey(jumpkey))
            {
                isJumping = true;
                jumpT = 0f;
            }
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
        else
        {
            if (speedBoost && !isJumping) stopT += Time.deltaTime;
            else movementT = 0f;
            //stopT += Time.deltaTime;

        }
        float speed = moveSpeed;
        if (speedBoost) speed *= speedBoostModifier;
        transform.position = transform.position + transform.forward * accelerationCurve.Evaluate(movementT) * speed * Time.deltaTime * deaccelerationCurve.Evaluate(stopT);
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

    
    
}
