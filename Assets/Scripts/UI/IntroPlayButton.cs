using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IntroPlayButton : MonoBehaviour
{
    public Button thisButton;
    public FadingIntroText thisText;
    // Update is called once per frame
    void Update()
    {
        if (thisText.sequenceCompleted) thisButton.enabled = true;
    }
}
