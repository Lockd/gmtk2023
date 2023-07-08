using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private InputAction interactAction;
    private CharacterMovement characterMovement;

    private void OnEnable()
    {
        interactAction.Enable();
    }

    private void OnDisable()
    {
        interactAction.Disable();
    }

    void Start()
    {
        characterMovement = GetComponent<CharacterMovement>();
        interactAction.performed += _ => OnInteract();
    }

    private void OnInteract()
    {
        Inventory.Instance.OnPickUp();
        characterMovement.OnEnterDoor();
    }

    private void FixedUpdate()
    {
        float xDirection = Input.GetAxis("Horizontal");
        characterMovement.OnMove(xDirection);
    }
}
