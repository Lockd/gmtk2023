using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckCompletedObjectives : MonoBehaviour
{

    public Transform objectives;

    public GameObject victoryScreen;

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
        if (completed) GameCompleted();
        else Debug.Log("Game Not Completed");
    }

    public void GameCompleted()
    {
        Time.timeScale = 0f;
        SceneManager.LoadScene("Game Win");
    }
}
