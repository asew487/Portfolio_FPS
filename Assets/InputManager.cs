using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 moveVector;
    public Vector2 look;
    public bool isSpint;
    public bool isJump;



    void Update()
    {
        MoveInput();
        LookInput();
        SprintInput();
        JumpInput();
    }

    void MoveInput()
    {
        float xValue = Input.GetAxis("Horizontal");
        float yValue = Input.GetAxis("Vertical");

        moveVector = new Vector2(xValue, yValue);
    }

    void LookInput()
    {
        float xValue = Input.GetAxisRaw("Mouse X");
        float yValue = Input.GetAxisRaw("Mouse Y");

        look = new Vector2(xValue, yValue);
    }

    void SprintInput()
    {
        isSpint = Input.GetKey(KeyCode.LeftShift);
    }

    void JumpInput()
    {
        isJump = Input.GetKeyDown(KeyCode.Space);
    }
}
