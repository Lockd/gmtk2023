using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Button playButton;

    public Button settingsButton;

    public Button backButton;

    public Button creditsBackButton;

    public GameObject settingsPosition;
    public Animator menu;
    public Animator settings;
    public Animator credits;


    public Button creditsButton;

    public Animator lightImage;

    public GameObject settingsUI;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(LaunchGame);
        creditsButton.onClick.AddListener(OpenCreditssMenu);
        settingsButton.onClick.AddListener(OpenSettingssMenu);
    }


    void OpenSettingssMenu()
    {
        menu.SetTrigger("FadeOut");
        lightImage.SetTrigger("FadeOut");
        settings.gameObject.SetActive(true);
        StartCoroutine(ShowSettings());
    }

    IEnumerator ShowSettings()
    {
        yield return new WaitForSeconds(1.5f);
        settings.SetTrigger("FadeIn");
        lightImage.SetTrigger("FadeIn");
        backButton.enabled = true;
    }

    void OpenCreditssMenu()
    {
        menu.SetTrigger("FadeOut");
        lightImage.SetTrigger("FadeOut");
        credits.gameObject.SetActive(true);
        StartCoroutine(ShowCredits());
    }

    IEnumerator ShowCredits()
    {
        yield return new WaitForSeconds(1.5f);
        credits.SetTrigger("FadeIn");
        lightImage.SetTrigger("FadeIn");
        creditsBackButton.enabled = true;
    }

    void CloseGame()
    {
        Application.Quit();
    }

    void LaunchGame()
    {
        SceneManager.LoadSceneAsync("Text Intro");
    }
}
