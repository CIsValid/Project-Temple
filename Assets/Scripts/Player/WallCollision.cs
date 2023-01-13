using System.Collections;
using System.Collections.Generic;
using UnityEngine;

    public enum E_PossibleRotations {
        None,
        Both,
        Right,
        Left
    }

public class WallCollision : MonoBehaviour
{
    public E_PossibleRotations possibleRotations;
    private PlayerMovement playerMovement;
    public int wallLayerIndex = 0;

    private void Start() 
    {
        playerMovement = transform.GetComponentInParent<PlayerMovement>();
        wallLayerIndex = LayerMask.NameToLayer("Wall");

    }

    private void OnTriggerStay(Collider other) 
    {
        if(other.gameObject.layer == wallLayerIndex)
        {
            playerMovement.stopForwardMovement = true;
        }

        if(other.CompareTag("TurnBoth")) {possibleRotations = E_PossibleRotations.Both;}
        if(other.CompareTag("TurnRight")) {possibleRotations = E_PossibleRotations.Right;}
        if(other.CompareTag("TurnLeft")) {possibleRotations = E_PossibleRotations.Left;}
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.gameObject.layer == wallLayerIndex)
        {
            playerMovement.stopForwardMovement = false;
        }
    }

}
