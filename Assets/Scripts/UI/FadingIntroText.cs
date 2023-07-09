using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FadingIntroText : MonoBehaviour
{
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
            yield return new WaitForSeconds(1.5f);
            i++;
        }
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("Val_TestScene");
    }
}
