using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    private Door targetDoor;
    private Collider2D colliderToDisable;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(float xDirection)
    {
        // TODO handle animation and sprite flipping
        Vector2 movement = new Vector2(xDirection, 0f) * moveSpeed * Time.deltaTime + new Vector2(0f, rb.velocity.y);

        rb.velocity = movement;
    }

    public void SetTargetDoor(Door door)
    {
        targetDoor = door;
    }

    public void OnEnterDoor()
    {
        if (targetDoor == null) return;

        // TODO disable controls, play animation
        targetDoor.OnEnter(transform);
    }
}
