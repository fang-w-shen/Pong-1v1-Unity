using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class MenuController : MonoBehaviour
{

    public static bool GameIsPaused = false;
    public static bool GameIsStarted = false;
    public GameObject pauseMenuUI;
    public GameObject mainMenuUI;
    public GameObject settingsMenuUI;
    public GameObject gameArea;

    private OptionsMenu optionsMenu;

    // Update is called once per frame
    void Update()
    {
        if (GameIsStarted && (Input.GetKeyDown(KeyCode.Escape) || Input.GetKeyDown(KeyCode.P)))
        {
            if (GameIsPaused == true)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }
    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        GameIsStarted = true;
        gameArea.SetActive(true);
        FindObjectOfType<GameManager>().PlayBall();
    }
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }


    public void LoadMenu()
    {
        pauseMenuUI.SetActive(false);
        mainMenuUI.SetActive(true);
        gameArea.SetActive(false);
        Time.timeScale = 0f;
        GameIsPaused = false;
        GameIsStarted = false;
        FindObjectOfType<GameManager>().EndGame();
    }
    public void LoadSettings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    public void Back()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
    public void Quit()
    {
        Application.Quit();
    }
}