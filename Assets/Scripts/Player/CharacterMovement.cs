using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(float xDirection)
    {
        // TODO handle animation and sprite flipping
        Vector2 movement = new Vector2(xDirection, 0f);

        rb.velocity = movement * moveSpeed * Time.deltaTime;
    }
}
