using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenuManager : MonoBehaviour
{
    bool paused;
    public Button resumeGame;
    public GameObject pauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        resumeGame.onClick.AddListener(UnPause);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !paused) Pause();
        else if (Input.GetKeyDown(KeyCode.Escape) && paused) UnPause();
    }

    void Pause()
    {
        paused = true;
        Time.timeScale = 0f;
        pauseMenu.SetActive(true);

    }

    void UnPause()
    {
        paused = false;
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
    }
}
