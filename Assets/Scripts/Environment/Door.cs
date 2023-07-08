using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    public Door linkedDoor;
    [SerializeField] private GameObject hintText;
    private CharacterMovement movement;

    private void Start()
    {
        hintText.SetActive(false);
    }

    public void OnEnter(Transform character)
    {
        // TODO play animation
        character.position = linkedDoor.transform.position;

        CameraSwitcher.Instance.SetLookAt(linkedDoor.transform, character);
    }

    private void OnTriggerStay2D(Collider2D other)
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
