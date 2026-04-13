using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    private PlayerMovement playerMovement;
    private GameManager gameManager;

    public GameObject pauseMenuCanvas;

    private bool isPaused = false;

    void Start()
    {
        playerMovement = FindObjectOfType<PlayerMovement>();
        gameManager = FindObjectOfType<GameManager>();

        pauseMenuCanvas.SetActive(false);

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
        pauseMenuCanvas.SetActive(true);

        foreach (AudioSource audioSource in gameManager.audioSources)
        {
            audioSource.Pause();
        }

        Time.timeScale = 0f;
        isPaused = true;
    }

    public void ResumeButton()
    {
        pauseMenuCanvas.SetActive(false);

        foreach (AudioSource audioSource in gameManager.audioSources)
        {
            audioSource.UnPause();
        }

        Time.timeScale = 1f;
        isPaused = false;
    }

    public void MainMenuButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}