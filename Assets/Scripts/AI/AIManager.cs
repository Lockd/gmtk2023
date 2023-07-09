using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class AIManager : MonoBehaviour
{

    public static AIManager instance { get; private set; }
    CharacterMovement movementScript;
    public List<AITarget> targets;
    public int backtrackingIndex;
    public AIStates currentState { get; private set; }
    public GameObject player;
    public float FOVDistance = 10f;
    public float FOVAngle = 90f;
    private SpriteRenderer _renderer;
    private bool isAlreadyScared = false;


    private int currentBacktrackingIndex;
    private bool pursuingTarget;

    public enum AIStates
    {
        Idle,
        Scared
    }

    private void Awake()
    {

        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start()
    {
        _renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        movementScript = GetComponent<CharacterMovement>();
        currentState = AIStates.Idle;
        currentBacktrackingIndex = backtrackingIndex;
        StartCoroutine(CheckLineOfSight());
    }

    private void Update()
    {
        switch (currentState)
        {
            case AIStates.Idle:
                if (targets.Count > 0)
                    PursueTarget();
                break;
            case AIStates.Scared:
                BeScared();
                break;

        }
    }

    private void BeScared()
    {
        if (isAlreadyScared) return;
        movementScript.animator.SetBool("haveNoticed", true);

        DOTween.KillAll();
        isAlreadyScared = true;

        Invoke(nameof(UnScare), 5f);
    }

    private void UnScare()
    {
        currentState = AIStates.Idle;
        isAlreadyScared = false;
        movementScript.animator.SetBool("haveNoticed", false);
    }

    private void PursueTarget()
    {
        if (pursuingTarget)
            return;
        pursuingTarget = true;
        AITarget target = targets[0];
        GameObject nextDestination = target.GetNextStop(currentState);
        movementScript.GoTo(nextDestination);
    }

    public void OnRouteDestinationReached()
    {
        AITarget target = targets[0];
        target.IncrementIndex();
        if (target.RouteFinished())
        {
            StartCoroutine(Wait());
            targets.Remove(target);
            targets.Add(target);
            target.ResetIndex();
            if (currentState == AIStates.Scared && --currentBacktrackingIndex == 0)
            {
                ReverseTargets();
                currentBacktrackingIndex = backtrackingIndex;
                currentState = AIStates.Idle;
            }
        }
        else
        {
            pursuingTarget = false;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(targets[0].waitingTime);
        pursuingTarget = false;
    }

    public void ScareInhabitant()
    {
        HealthManagement.Instance.OnChnageHealth();

        int scareIndex = HealthManagement.Instance.maxHealth - HealthManagement.Instance.currentHealth;
        FMODUnity.RuntimeManager.PlayOneShot($"event:/SFX/Granny/Scare/Granny_Scare_0{scareIndex}");
        // TODO add animator trigger here
        currentState = AIStates.Scared;
        pursuingTarget = false;
        ReverseTargets();
    }

    private void ReverseTargets()
    {
        targets.Reverse();
        foreach (AITarget target in targets)
        {
            for (int i = 0; i < target.route.Count; i++)
            {
                Door door = target.route[i].GetComponent<Door>();
                if (door != null)
                {
                    target.route[i] = door.linkedDoor.gameObject;
                }
            }
        }
    }

    private IEnumerator CheckLineOfSight()
    {
        WaitForSeconds wait = new WaitForSeconds(0.2f);

        while (true)
        {
            yield return wait;
            if (player.GetComponent<CharacterMovement>().isHidden)
                continue;
            Vector3 directionToPlayer = (player.transform.position - transform.position);
            Vector2 lookingDirection = new Vector2(_renderer.flipX ? 1f : -1f, 0f);
            float angleToPlayer = Vector2.Angle(lookingDirection, directionToPlayer);
            if (angleToPlayer > FOVAngle / 2)
            {
                continue;
            }
            if (directionToPlayer.magnitude < FOVDistance && currentState != AIStates.Scared)
            {
                PlayerBubbles.Instance.AddBubble("Uh-oh, I am sorry, I should run!!!");
                AIManager.instance.ScareInhabitant();
            }
        }
    }

}
