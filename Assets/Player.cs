using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public GameObject cam;
    public float rotationTime;
    public float moveSpeed;

    private float rotateVelocity;
    private float targetRotation;

    InputManager input;
    CharacterController controller;
    

    void Start()
    {
        input = GetComponent<InputManager>();
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Move();
        //jump
        //camera
    }

    void Move()
    {
        Vector3 moveVector = new Vector3(input.moveVector.x, 0, input.moveVector.y).normalized;
        if (moveVector !=  Vector3.zero)
        {
            targetRotation = Mathf.Atan2(moveVector.x, moveVector.z) * Mathf.Rad2Deg + cam.transform.eulerAngles.y;

            float smoothRotation = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref rotateVelocity, rotationTime);

            transform.rotation = Quaternion.Euler(0, smoothRotation, 0);
        }

        Vector3 moveDirection = Quaternion.Euler(0, targetRotation, 0) * Vector3.forward;

        if (moveVector != Vector3.zero)
        {
            controller.Move(moveDirection.normalized * moveSpeed * Time.deltaTime);
        }
    }
}
