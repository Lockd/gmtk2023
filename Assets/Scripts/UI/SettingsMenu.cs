using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public Button backButton;

    public GameObject menuUI;

    public GameObject menuPosition;

    public Camera mainCamera;

    MenuCameraMovement thisCamera;
    // Start is called before the first frame update
    void Start()
    {

        thisCamera = mainCamera.GetComponent<MenuCameraMovement>();
        backButton.onClick.AddListener(ReturnToMainMenu);
    }

    void ReturnToMainMenu()
    {
        thisCamera.isMoving = true;
        thisCamera.moveTo = menuPosition;
        thisCamera.direction = -1;
    }
}
