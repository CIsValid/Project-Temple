using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    /* Created functions as virtual void so they may be overriden as needed. (For example a power up)
    There is also an obvious flaw in this currently. Moving the player using transform allows the player to force their way through walls.
    We can solve this by using the rigidbody to move the player instead.*/
    public virtual void MoveForward()
    {
        this.gameObject.transform.position += transform.forward * Time.deltaTime * speed;

    }

    public virtual void MoveRight(bool bMoveRight)
    {
        if(bMoveRight) 
        {
            this.gameObject.transform.position += transform.right * Time.deltaTime * sideSpeed;
            previousMoveInput = E_PreviousMoveInput.Right;
            return;
        }

        this.gameObject.transform.position -= transform.right * Time.deltaTime * sideSpeed;
        previousMoveInput = E_PreviousMoveInput.Left;
        return;

    }

    public virtual void Jump()
    {
        //@ Implement launching player into air.

        previousMoveInput = E_PreviousMoveInput.Jump;

    }

    public virtual void Slide()
    {
        //@ Implement sliding allowing for passing under tall objects or pathways.

        previousMoveInput = E_PreviousMoveInput.Slide;

    }
}
