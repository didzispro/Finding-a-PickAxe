using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager gameManager;

    public TextMeshProUGUI pausedText;

    public Button resumeButton;
    public Button mainMenuButton;
    public Button quitButton;

    private bool isPaused = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

        // Hide pause menu at start
        pausedText.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && playerMovement.isGameActive)
        {
            if (isPaused)
            {
                ResumeButton();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        pausedText.gameObject.SetActive(true);
        resumeButton.gameObject.SetActive(true);
        mainMenuButton.gameObject.SetActive(true);
        quitButton.gameObject.SetActive(true);

        gameManager.musicSource.Stop();

        Time.timeScale = 0f; // PAUSE GAME
        isPaused = true;
    }

    public void ResumeButton()
    {
        pausedText.gameObject.SetActive(false);
        resumeButton.gameObject.SetActive(false);
        mainMenuButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);

        gameManager.musicSource.Play();

        Time.timeScale = 1f; // RESUME GAME
        isPaused = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f; // IMPORTANT so menu is not frozen
        SceneManager.LoadScene("MainMenu"); // change to your menu scene name
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}