using UnityEngine;
using System.Collections.Generic;

public class AITarget : MonoBehaviour {

    public List<GameObject> route;
    public float waitingTime;   // in seconds
    public int routeIndex { get; private set; } = 0;

    public GameObject GetNextStop(AIManager.AIStates state) {
        if(state == AIManager.AIStates.Idle)
            return route[routeIndex];
        if (state == AIManager.AIStates.Scared) {
            int reverseIndex = route.Count - routeIndex - 1;
            return route[reverseIndex];
        }
        return null;
    }

    public void IncrementIndex() {
        routeIndex++;
    }

    public void ResetIndex() {
        routeIndex = 0;
    }

    public bool RouteFinished() {
        return route.Count == routeIndex;
    }
}