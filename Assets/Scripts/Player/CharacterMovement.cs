using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float accelerationSpeed;
    public Door targetDoor;
    public HideableObject targetHidableObject;
    public GameObjective targetObjective;
    private Collider2D colliderToDisable;
    private Rigidbody2D rb;
    public Animator animator;
    [SerializeField] private SpriteRenderer _renderer;
    public bool canMove;
    public bool isHidden = false;
    [SerializeField] private Collider2D _collider;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(float xDirection)
    {
        animator.SetBool("isRunning", xDirection != 0 && canMove);
        if (!canMove) return;

        if (xDirection != 0f) { _renderer.flipX = xDirection > 0f; }

        // TODO handle animation and sprite flipping
        Vector2 movement = new Vector2(xDirection, 0f) * moveSpeed * Time.deltaTime + new Vector2(0f, rb.velocity.y);

        rb.velocity = movement;
    }

    internal void GoTo(GameObject destination)
    {
        animator.SetBool("isRunning", true);
        FMODUnity.RuntimeManager.PlayOneShot("event:/SFX/Granny/Granny_Footsteps");
        float xDestination = destination.transform.position.x;
        _renderer.flipX = (xDestination - transform.position.x) > 0;
        float length = Math.Abs(xDestination - transform.position.x);
        float duration = length / moveSpeed;
        transform.DOMoveX(xDestination, duration).OnComplete(() => OnDestinationReached(destination));
    }

    private void OnDestinationReached(GameObject destination)
    {
        animator.SetBool("isRunning", false);
        Door door = destination.GetComponent<Door>();
        if (door != null)
            door.OnEnter(transform);

        AIManager.instance.OnRouteDestinationReached();
    }

    public void SetTargetDoor(Door door)
    {
        targetDoor = door;
    }

    public void ChangeMoveAbility(bool value)
    {
        rb.velocity = Vector2.zero;
        canMove = value;
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

    public void SetTargetGameObjective(GameObjective targetObjective)
    {
        this.targetObjective = targetObjective;
    }

    public void OnInteractWithGameObjective()
    {
        if (targetObjective == null) return;

        targetObjective.OnInteract();
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
        rb.velocity = Vector2.zero;
        animator.SetBool("isHidden", true);
        targetHidableObject.OnHide();
        canMove = false;
        isHidden = true;
        _collider.isTrigger = true;
    }

    public void OnLeaveHidableObject()
    {
        animator.SetBool("isHidden", false);
        targetHidableObject.OnLeave();
        canMove = true;
        isHidden = false;
        _collider.isTrigger = false;
    }
}
