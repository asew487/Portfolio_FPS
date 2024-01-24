using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cam;
    public float rotationTime;
    public float moveSpeed;
    public GameObject cameraTargetObj;
    public GameObject head;

    public float gravity;
    public float jumpTime;
    public float fallTime;
    public float jumpHeight;

    public bool isGround = true;
    public float groundedOffset;
    public float sphereRadius;
    public LayerMask layerMask;

    private float targetRotation;
    private float cameraTargetYaw = 0;
    private float cameraTargetPitch = 0;
    private float verticalVelocity;

    InputManager input;
    CharacterController controller;
    Animator animator;
    

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        input = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        GroundCheck();
        JumpAndGravity();
        Move();
        Animation();
    }

    private void LateUpdate()
    {
        CameraRotation();
    }

    void Move()
    {
        float _moveSpeed = input.moveVector != Vector2.zero ? moveSpeed : 0;

        Vector3 moveVector = new Vector3(input.moveVector.x, 0, input.moveVector.y).normalized;

        if (input.moveVector !=  Vector2.zero)
        {
            targetRotation = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;
        }

        Vector3 moveDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

        controller.Move(moveDirection.normalized * _moveSpeed * Time.deltaTime +
                            new Vector3(0, verticalVelocity, 0) * Time.deltaTime);
    }

    void CameraRotation()
    {
        if (input.look != Vector2.zero)
        {
            cameraTargetYaw += input.look.x;
            cameraTargetPitch += -input.look.y;
        }

        cameraTargetYaw = ClampAngle(cameraTargetYaw, float.MinValue, float.MaxValue);
        cameraTargetPitch = ClampAngle(cameraTargetPitch, -40f, 40f);
        float headPitch = ClampAngle(cameraTargetPitch, -40f, 40f);

        cameraTargetObj.transform.rotation = Quaternion.Euler(0, cameraTargetYaw, 0);
        cam.transform.rotation = Quaternion.Euler(cameraTargetPitch, cameraTargetYaw, 0);
        head.transform.rotation = Quaternion.Euler(headPitch, cameraTargetYaw, 0);
    }

    void GroundCheck()
    {
        Vector3 spherePosition = new Vector3(transform.position.x, transform.position.y - groundedOffset, transform.position.z);
        isGround = Physics.CheckSphere(spherePosition, sphereRadius, layerMask, QueryTriggerInteraction.Ignore);
    }

    void JumpAndGravity()
    {
        if(isGround)
        {
            if(verticalVelocity <= 0)
            {
                verticalVelocity = -2;
            }

            if(input.isJump)
            {
                verticalVelocity = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
        }

        verticalVelocity += gravity * Time.deltaTime;
    }

    void Animation()
    {
        animator.SetFloat("Horizontal", input.moveVector.x);
        animator.SetFloat("Vertical", input.moveVector.y);
    }

    float ClampAngle(float value, float min, float max)
    {
        if (value < -360f) value += 360;
        if (value > 360f) value -= 360;

        return Mathf.Clamp(value, min, max);
    }
}
