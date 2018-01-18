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
    /// <summary>
    /// Starts GameBoard Canvas Render and GamePlay
    /// </summary>
    public void StartGame()
    {
        mainMenuUI.SetActive(false);
        GameIsStarted = true;
        gameArea.SetActive(true);
        FindObjectOfType<GameManager>().PlayBall();
    }
    /// <summary>
    /// Pauses all animation events and provides menu for options
    /// </summary>
    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }
    /// <summary>
    /// Resumes all animation events and gameplay
    /// </summary>
    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    /// <summary>
    /// Loads main menu without reloading scene (to keep volume the same)
    /// </summary>
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
    /// <summary>
    /// Loads settings to change volume, resolution, graphics, fullScreen
    /// </summary>
    public void LoadSettings()
    {
        mainMenuUI.SetActive(false);
        settingsMenuUI.SetActive(true);
    }
    /// <summary>
    /// Backs out of settings to main menu
    /// </summary>
    public void Back()
    {
        mainMenuUI.SetActive(true);
        settingsMenuUI.SetActive(false);
    }
    /// <summary>
    /// Quits application (does not work for WEB-builds)
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }
}