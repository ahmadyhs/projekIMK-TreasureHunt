using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using AH = AdnanHelper;

public class PlayerController : MonoBehaviour
{
    //public float rotation;
    public float moveSpeed;
    private float originalMoveSpeed;
    public float speedBoostModifier;
    public float speedBoostRotationModifer;
    public float jumpspeed = 1f;
    private float originalJumpSpeed;
    private bool onGround = false;
    public bool jumpBoost = false;
    public bool speedBoost = false;
    public float rotationSpeed=1f;
    public Quaternion rotationTarget;
    public KeyCode jumpkey = KeyCode.Space;
    public float movementT;
    public float stopT;
    public float jumpT;
    bool isDirectionalKeyPressed = false;
    public AnimationCurve accelerationCurve;
    public AnimationCurve deaccelerationCurve;
    public AnimationCurve jumpAccelerationCurve;
    public AnimationCurve jumpBoostAccelerationCurve;
    bool isJumping = false;
    public Vector3 direction;
    public float acceleration = 0f;
    //public GameObject penggerakLeher;

    // Start is called before the first frame update
    void Start()
    {
        originalMoveSpeed = moveSpeed;
        originalJumpSpeed = jumpspeed;
        movementT = 0f;
        jumpT = 0.38f;
        stopT = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        acceleration = accelerationCurve.Evaluate(movementT);
        updateDirection();
        isDirectionalKeyPressed = direction != Vector3.zero;
        checkJump();
        updateRotationTarget();
    }
    private void LateUpdate()
    {
        updateRotation();
        updatePosition();
    }

    private void updateRotationTarget()
    {
        if (isDirectionalKeyPressed)
        {
            rotationTarget = Quaternion.LookRotation(direction, transform.up);
        }
    }
    private void updateDirection()
    {
        direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        //penggerakLeher.transform.position = AH.lerp(penggerakLeher.transform.position, transform.position+direction*3f, 1f, 0f, 1f);
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
    private void updateRotation()
    {
        float speed = Time.deltaTime * rotationSpeed;
        if (speedBoost) speed = 360f;
        transform.rotation = Quaternion.RotateTowards(transform.rotation, rotationTarget, speed);
    }
    private void updatePosition()
    {
        if (isJumping)
        {
            movementT = 1f;
            AnimationCurve curve;
            if (jumpBoost) curve = jumpBoostAccelerationCurve;
            else curve = jumpAccelerationCurve;
            transform.position = transform.position + transform.up * curve.Evaluate(jumpT);
        }
        //else jjumpspeed= 0f;
        jumpT += Time.deltaTime * jumpspeed;
        if (isDirectionalKeyPressed)
        {
            stopT = 0f;
            movementT += Time.deltaTime;
            movementT = Mathf.Clamp01(movementT);
        }
        else
        {
            if (speedBoost && !isJumping) stopT += Time.deltaTime;
            else movementT = 0f;
            //stopT += Time.deltaTime;

        }
        float speed = moveSpeed;
        if (speedBoost) speed *= speedBoostModifier;
        transform.position = transform.position + transform.forward * acceleration * speed * Time.deltaTime * deaccelerationCurve.Evaluate(stopT);
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isJumping = false;
            onGround = true;
            transform.position = new Vector3(transform.position.x, other.transform.position.y, transform.position.z);
        }
        if (other.gameObject.tag == "box")
        {
            if (onGround)
                moveSpeed = 0f;
            else 
                transform.position = new Vector3(transform.position.x, other.GetContact(1).point.y, transform.position.z);
            isJumping = false;
            if (isJumping)
            {
              //  jumpspeed = 0f;
            }
        }

    }
    private void OnCollisionExit(Collision other)
    {
        if (other.gameObject.tag == "ground")
        {
            isJumping = true;
            onGround = false;
        }
        if (other.gameObject.tag == "box")
        {
            moveSpeed = originalMoveSpeed;
            //jumpspeed = originalJumpSpeed;
            if (!onGround)
                isJumping = true;
        }
    }

    
    
}
