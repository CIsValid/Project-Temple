using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class MovementHandler : MonoBehaviour
{
    /*@ Need to implement a check after completing an action. 
    For example rotating the character and camera if we moved right or left and there is a wall infront of us.
    */

    public enum E_PreviousMoveInput {
        None,
        Jump,
        Slide,
        Right,
        Left
    }

    public E_PreviousMoveInput previousMoveInput = E_PreviousMoveInput.None;

    // The speed which the player moves forward every second.
    public float speed = 1f;

    // The speed which character moves to the right or left.
    public float sideSpeed = 15f;
    public float jumpHeight = 0.3f;
    public bool bCanJump = true;


    private CharacterController _characterController = null;  

    private Vector3 playerVelocity;

    private bool isPlayerGrounded;

    private float gravityValue = -9.81f;

    private void Start() {
        _characterController = GetComponent<CharacterController>();
    }


    /* Created functions as virtual void so they may be overriden as needed. (For example a power up)
    There is also an obvious flaw in this currently. Moving the player using transform allows the player to force their way through walls.
    We can solve this by using the rigidbody to move the player instead.*/
    public virtual void MoveForward()
    {
        var direction = transform.TransformDirection(Vector3.forward);
        _characterController.SimpleMove(direction * speed);

    }

    public virtual void MoveRight(bool bMoveRight)
    {
        if(bMoveRight) 
        {
            previousMoveInput = E_PreviousMoveInput.Right;
            return;
        }

        previousMoveInput = E_PreviousMoveInput.Left;
        return;

    }

    public virtual void Jump()
    {
        
        playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        previousMoveInput = E_PreviousMoveInput.Jump;

        bCanJump = false;

    }

    public virtual void Slide()
    {
        //@ Implement sliding allowing for passing under tall objects or pathways.

        previousMoveInput = E_PreviousMoveInput.Slide;

    }

    public void UpdateMovement()
    {
        GroundedCheck();

        TranslateInput();

        GravityHandle();

    }
    private void GroundedCheck()
    {
        isPlayerGrounded = _characterController.isGrounded && playerVelocity.y < 0;
        if (isPlayerGrounded)
        {
            playerVelocity.y = 0f;
            bCanJump = true;
        }

    }

    private void TranslateInput()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);
        Vector3 worldInputMovement = transform.TransformDirection(move.normalized);
        _characterController.Move(worldInputMovement * Time.deltaTime * speed);

    }

    private void GravityHandle()
    {
        playerVelocity.y += gravityValue * Time.deltaTime;
        _characterController.Move(playerVelocity * Time.deltaTime);
    }
}
