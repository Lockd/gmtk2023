using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    private CharacterMovement characterMovement;

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();

    }

    void FixedUpdate()
    {
        // Movement Logic
        float xDirection = Input.GetAxis("Horizontal");
        characterMovement.OnMove(xDirection);

        if (Input.GetKeyDown(KeyCode.E))
        {
            // TODO this logic is hacky? 

            // Pickup Logic
            Inventory.Instance.OnPickUp();

            // Door Logic
            characterMovement.OnEnterDoor();
        }

        // TODO Hide logic
        // TODO Ability logic
    }
}
