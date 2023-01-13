using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MovementHandler
{

    public bool stopForwardMovement;

    // Update is called once per frame
    void Update()
    {
        UpdateMovement();

        if(!stopForwardMovement) MoveForward();

        //@ Currently using these inputs to debug the movement to get it right. Implement properly using the touch screen input later.

        if(Input.GetKey(KeyCode.D)) 
        {
            MoveRight(true);
        }

        if(Input.GetKey(KeyCode.A)) 
        {
            MoveRight(false);
        }

        if(Input.GetKey(KeyCode.S)) 
        {
            Slide();
        }

        if(Input.GetKey(KeyCode.Space) && bCanJump) 
        {
            Jump();
        }

        if(Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D) || Input.GetKeyUp(KeyCode.S) )
        {
            previousMoveInput = E_PreviousMoveInput.None;
        }

    }
}
