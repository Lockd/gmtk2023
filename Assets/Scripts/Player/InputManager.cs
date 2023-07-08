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

        // Pickup Logic
        if (Input.GetKeyDown(KeyCode.E))
        {
            Inventory.Instance.OnPickUp();
        }

        // TODO Hide logic
        // TODO Ability logic
    }
}
