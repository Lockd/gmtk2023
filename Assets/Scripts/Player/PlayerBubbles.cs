using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerBubbles : MonoBehaviour
{
    [SerializeField] private float timeToDisplayBubble;
    [SerializeField] private TMP_Text text;
    [SerializeField] private GameObject textContainer;
    private static PlayerBubbles _instance;
    public static PlayerBubbles Instance { get { return _instance; } }
    private void Awake()
    {
        if (_instance != null && _instance != this) Destroy(this.gameObject);
        else _instance = this;
    }

    private void Start()
    {
        text.gameObject.SetActive(false);
        textContainer.SetActive(false);
    }

    public void AddBubble(string message)
    {
        StartCoroutine(BubbleLogic(message));
    }

    public void ClearBubbles()
    {
        StopAllCoroutines();
        textContainer.SetActive(false);
        text.gameObject.SetActive(false);
    }

    private IEnumerator BubbleLogic(string message)
    {
        textContainer.SetActive(true);
        yield return new WaitForSeconds(.1f);
        text.text = message;
        text.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeToDisplayBubble);
        textContainer.SetActive(false);
        text.gameObject.SetActive(false);
    }
}
