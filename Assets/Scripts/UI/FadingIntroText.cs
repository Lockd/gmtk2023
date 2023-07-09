using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FadingIntroText : MonoBehaviour
{
    [SerializeField] private string sceneToLoad = "Main Game";
    [SerializeField] private float finalWaitTime = 5f;
    [SerializeField] private float timeBetweenText = 1.5f;
    public List<Animator> sequence;
    int i = 0;

    private void Start()
    {
        StartCoroutine(somecoroutine());
    }

    private IEnumerator somecoroutine()
    {
        while (i < sequence.Count)
        {
            sequence[i].SetTrigger("FadeIn");
            yield return new WaitForSeconds(timeBetweenText);
            i++;
        }
        yield return new WaitForSeconds(finalWaitTime);
        SceneManager.LoadScene(sceneToLoad);
    }
}
