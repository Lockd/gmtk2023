using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckCompletedObjectives : MonoBehaviour
{

    public Transform objectives;

    public void CheckGameCompletionStatus()
    {
        bool completed = true;
        foreach (Transform t in objectives)
        {
            if (!t.gameObject.GetComponent<GameObjective>().isComplete)
            {
                completed = false;
                break;
            }
        }
        if(completed) GameCompleted();
        else Debug.Log("Game Not Completed");
    }

    public static void GameCompleted()
    {
        Debug.Log("Game Completed");
    }
}
