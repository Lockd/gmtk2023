using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class FadingIntroText : MonoBehaviour
{
    public List<string> textLines;
    public TextMeshProUGUI text;

    float nextTextChange = 0f;
    float textChangeTime = 6.4f;
    int i = 0;
    // Start is called before the first frame update

    private void Update()
    {
        if (Time.time > nextTextChange)
        {
            nextTextChange += textChangeTime;
            if (i < textLines.Count)
                text.text = textLines[i];
            else SceneManager.LoadScene("Val_TestScene");
            i++;
        }
    }
}
