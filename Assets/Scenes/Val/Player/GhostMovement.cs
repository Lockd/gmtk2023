using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GhostMovement : MonoBehaviour
{
    //Public for testing purposes
    public float moveSpeed;

    public Rigidbody2D thisRb;

    Vector2 movementInput;

    // Update is called once per frame
    void FixedUpdate()
    {
        movementInput.x = Input.GetAxisRaw("Horizontal");
        movementInput.y = Input.GetAxisRaw("Vertical");
        movementInput.Normalize();
        thisRb.velocity = movementInput * moveSpeed;

        //Turns the player to the left and right based on movementInput.x value;
        int xScale = movementInput.x >= 0 ? 1 : -1;
        transform.localScale = new Vector2(xScale, 1f);


    }
}
