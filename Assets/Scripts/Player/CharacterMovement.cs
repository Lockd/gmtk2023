using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    public Door targetDoor;
    public HideableObject targetHidableObject;
    private Collider2D colliderToDisable;
    private Rigidbody2D rb;
    private Animator animator;
    private bool canMove = true;
    public bool isHidden = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(float xDirection)
    {
        if (!canMove) return;

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

        targetDoor.OnEnter(transform);
        animator.SetTrigger("fadeIn");
    }

    public void SetTargetHidableObject(HideableObject hideableObject)
    {
        targetHidableObject = hideableObject;
    }

    public void OnChangeHiddenStatus()
    {
        if (targetHidableObject == null) return;

        if (canMove) OnHide();
        else OnLeaveHidableObject();
    }

    public void OnHide()
    {
        if (targetHidableObject == null) return;

        animator.SetBool("isHidden", true);
        targetHidableObject.OnHide();
        canMove = false;
        isHidden = true;
    }

    public void OnLeaveHidableObject()
    {
        animator.SetBool("isHidden", false);
        targetHidableObject.OnLeave();
        canMove = true;
        isHidden = false;
    }
}
