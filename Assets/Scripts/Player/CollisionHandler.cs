using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    public GameObject collisionChecker = null;
    public float turnSpeed = 6f;

    private PlayerMovement playerMovementRef = null;
    private WallCollision wallCollisionRef = null;
    private Quaternion targetRotation = Quaternion.Euler(0,0,0);
    private float rotationAmount = 90;
    private bool bReachedDest = true;


    private void Start() 
    {
        playerMovementRef = GetComponent<PlayerMovement>();
        wallCollisionRef = collisionChecker.GetComponent<WallCollision>();
        targetRotation = transform.rotation;

    }

    private void Update() {UpdateRotation();}

    //@ Not the most optimal solution due to the time complexity so revisit this later.
    private void UpdateRotation()
    {
        if(bReachedDest) 
        {
            switch(wallCollisionRef.possibleRotations)
            {
                case E_PossibleRotations.Both:

                    switch(playerMovementRef.previousMoveInput)
                    {
                        case MovementHandler.E_PreviousMoveInput.Right:
                        TurnPlayerRight(true);
                        break;

                        case MovementHandler.E_PreviousMoveInput.Left:
                        TurnPlayerRight(false);
                        break;

                    }
                
                break;
                case E_PossibleRotations.Right:
                if(playerMovementRef.previousMoveInput == MovementHandler.E_PreviousMoveInput.Right)
                {
                    TurnPlayerRight(true);
                    break;
                }
                break;
                case E_PossibleRotations.Left:
                if(playerMovementRef.previousMoveInput == MovementHandler.E_PreviousMoveInput.Left)
                {
                    TurnPlayerRight(false);
                    break;
                }
                break;
            
            }
        }


        // Lerp from our current rotation to the new and if we have reached it then we wish to reset our rotation state.
        if(!bReachedDest) 
        {
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
            if(transform.rotation != targetRotation) return;
            ResetPlayerState();
            return;
        }
    }
        
    // Here we control the rotation of our player.
    private void TurnPlayerRight(bool bMoveRight)
    {
        float newRotation = 0;

        if(bMoveRight)
        {
            newRotation = transform.eulerAngles.y + rotationAmount;
            targetRotation = Quaternion.Euler(targetRotation.x, newRotation, targetRotation.z);
            bReachedDest = false;
            return;

        }

            newRotation = transform.eulerAngles.y - rotationAmount;
            targetRotation = Quaternion.Euler(targetRotation.x, newRotation, targetRotation.z);
            bReachedDest = false;
            return;

    }

    // Temporary method to reset the rotation state to prevent multiple rotations happening on 1 wall.
    private void ResetPlayerState()
    {
        playerMovementRef.previousMoveInput = MovementHandler.E_PreviousMoveInput.None;
        wallCollisionRef.possibleRotations = E_PossibleRotations.None;
        bReachedDest = true;
    }

}
