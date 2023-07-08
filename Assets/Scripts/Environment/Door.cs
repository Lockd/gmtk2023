using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private Door linkedDoor;
    [SerializeField] private GameObject hintText;
    private CharacterMovement movement;

    public void OnEnter(Transform character)
    {
        // TODO play animation
        character.position = linkedDoor.transform.position;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (movement == null) movement = other.GetComponent<CharacterMovement>();

            movement.SetTargetDoor(this);
            hintText.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            movement.SetTargetDoor(null);
            hintText.SetActive(false);
        }
    }
}
