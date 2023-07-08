using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public static CharacterMovement instance;
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    public Door targetDoor;
    public HideableObject targetHidableObject;
    private Transform transform;
    private Collider2D colliderToDisable;
    private Rigidbody2D rb;
    private Animator animator;
    [SerializeField] private SpriteRenderer _renderer;
    private bool canMove = true;
    public bool isHidden = false;

    private void Awake() {
        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        transform = GetComponent<Transform>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    public void OnMove(float xDirection)
    {
        if (!canMove) return;

        if (xDirection != 0f) _renderer.flipX = xDirection > 0f;

        // TODO handle animation and sprite flipping
        Vector2 movement = new Vector2(xDirection, 0f) * moveSpeed * Time.deltaTime + new Vector2(0f, rb.velocity.y);

        rb.velocity = movement;
    }

    internal void GoTo(GameObject destination) {
        float xDestination = destination.transform.position.x;
        float length = Math.Abs(xDestination - transform.position.x);
        float duration = length / moveSpeed;
        transform.DOMoveX(xDestination, duration).OnComplete(() => OnDestinationReached(destination));
    }

    private void OnDestinationReached(GameObject destination) {
        Door door = destination.GetComponent<Door>();
        if(door != null)
            door.OnEnter(transform);
        AIManager.instance.OnRouteDestinationReached();
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
