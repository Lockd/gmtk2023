using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenuManager : MonoBehaviour
{
    public Button backButton;

    public GameObject mainMenuObject;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void ReturnToMainMenu()
    {
        mainMenuObject.SetActive(true);
        gameObject.SetActive(false);
    }
}
