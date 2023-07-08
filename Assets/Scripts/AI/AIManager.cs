using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public static AIManager instance { get; private set; }
    public List<AITarget> targets;
    public int backtrackingIndex;
    public AIStates currentState { get; private set; }

    private int currentBacktrackingIndex;
    private bool pursuingTarget;

    public enum AIStates {
        Idle,
        Scared
    }

    private void Awake() {

        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        currentState = AIStates.Idle;
        currentBacktrackingIndex = backtrackingIndex;
    }

    private void Update() {
        switch(currentState) {
            case AIStates.Idle:
                if(targets.Count > 0)
                    PursueTarget();
                break;
            case AIStates.Scared:
                PursueTarget();
                break;

        }
    }

    private void PursueTarget() {
        if (pursuingTarget)
            return;
        pursuingTarget = true;
        AITarget target = targets[0];
        GameObject nextDestination = target.GetNextStop(currentState);
        CharacterMovement.instance.GoTo(nextDestination);
    }

    public void OnRouteDestinationReached() {
        AITarget target = targets[0];
        target.IncrementIndex();
        if (target.RouteFinished()) { 
            StartCoroutine(Wait());
            targets.Remove(target);
            targets.Add(target);
            target.ResetIndex();
            if (currentState == AIStates.Scared && --currentBacktrackingIndex == 0) {
                ReverseTargets();
                currentBacktrackingIndex = backtrackingIndex;
                currentState = AIStates.Idle;
            }   
        } else {
            pursuingTarget = false;
        }
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(targets[0].waitingTime);
        pursuingTarget = false;
    }

    private void ScareInhabitant() {
        currentState = AIStates.Scared;
        ReverseTargets();
    }

    private void ReverseTargets() {
        targets.Reverse();
        foreach (AITarget target in targets) {
            for (int i = 0; i < target.route.Count; i++) {
                Door door = target.route[i].GetComponent<Door>();
                if (door != null) {
                    target.route[i] = door.linkedDoor.gameObject;
                }
            }
        }
    }

}
