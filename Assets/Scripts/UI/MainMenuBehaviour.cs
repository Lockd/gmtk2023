using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuBehaviour : MonoBehaviour
{
    public Button playButton;

    public Button settingsButton;

    public GameObject settingsMenu;

    public Camera mainCamera;

    public GameObject settingsImage;

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
        settingsImage.SetActive(true);
        gameObject.SetActive(false);
        //mainCamera.transform.position = new Vector3(savesImage.transform.position.x, savesImage.transform.position.y, mainCamera.transform.position.z);
       // thisCamera.isMoving = true;
       // thisCamera.moveTo = settingsImage;
        //thisCamera.direction = -1;
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
