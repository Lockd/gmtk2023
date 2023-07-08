using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Button playButton;

    public Button settingsButton;

    public GameObject settingsPosition;

    public Camera mainCamera;

    public GameObject settingsUI;

    MenuCameraMovement thisCamera;
    // Start is called before the first frame update
    void Start()
    {
        playButton.onClick.AddListener(LaunchGame);

        settingsButton.onClick.AddListener(OpenSettingssMenu);

        thisCamera = mainCamera.GetComponent<MenuCameraMovement>();
    }


    void OpenSettingssMenu()
    {
        //settingsUI.SetActive(true);
        //gameObject.SetActive(false);
        //mainCamera.transform.position = new Vector3(settingsPosition.transform.position.x, settingsPosition.transform.position.y, mainCamera.transform.position.z);
        thisCamera.isMoving = true;
        thisCamera.moveTo = settingsPosition;
        thisCamera.direction = 1;
        /*
        savesMenu.SetActive(true);
        gameObject.SetActive(false);
        */

    }

    void CloseGame()
    {
        Application.Quit();
    }

    void LaunchGame()
    {
        SceneManager.LoadScene("Val_TestScene");
    }
}
