using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button backButton;

    public Animator menu;
    public Animator settings;

    public Animator lightImage;

    public GameObject menuPosition;

    public Camera mainCamera;

    MenuCameraMovement thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    void ReturnToMainMenu()
    {
        backButton.enabled = false;
        settings.SetTrigger("FadeOut");
        lightImage.SetTrigger("FadeOut");
        StartCoroutine(ShowMenu());
    }



    IEnumerator ShowMenu()
    {
        yield return new WaitForSeconds(1.5f);
        menu.SetTrigger("FadeIn");
        lightImage.SetTrigger("FadeIn");

        gameObject.SetActive(false);
    }
}
