using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public Vector2 moveVector;
    public bool isSpint;
    public bool isJump;



    void Update()
    {
        MoveInput();
        SprintInput();
        JumpInput();
    }

    void MoveInput()
    {
        float xValue = Input.GetAxisRaw("Horizontal");
        float yValue = Input.GetAxisRaw("Vertical");

        moveVector = new Vector2(xValue, yValue);
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
