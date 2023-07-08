using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour {

    public static AIManager instance { get; private set; }
    public List<AITarget> targets;

    private AIStates currentState;
    private bool pursuingTarget;

    public enum AIStates {
        Wandering
    }

    private void Awake() {

        if (instance != null && instance != this)
            Destroy(this);
        else
            instance = this;
    }

    private void Start() {
        currentState = AIStates.Wandering;
    }

    private void Update() {
        switch(currentState) {
            case AIStates.Wandering:
                if(targets.Count > 0)
                    PursueTarget();
                break;
        }
    }

    private void PursueTarget() {
        if (pursuingTarget)
            return;
        AITarget currentTarget = targets[0];
        List<GameObject> currentRoute = currentTarget.route;
        GameObject nextDestination = currentRoute[0];
        CharacterMovement.instance.GoTo(nextDestination);
        pursuingTarget = true;
    }

    public void OnRouteDestinationReached() {
        AITarget currentTarget = targets[0];
        List<GameObject> currentRoute = currentTarget.route;
        currentRoute.Remove(currentRoute[0]);
        if (currentRoute.Count == 0) { 
            StartCoroutine(Wait());
            targets.Remove(currentTarget);
        } else {
            pursuingTarget = false;
        }
    }

    private IEnumerator Wait() {
        yield return new WaitForSeconds(targets[0].waitingTime);
        pursuingTarget = false;
    }

}
