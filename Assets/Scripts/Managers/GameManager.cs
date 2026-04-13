using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Space(5)]
    public GameObject textCanvas;
    [Space(10)]
    public GameObject winTextCanvas;

    [Header("Music Sources")]
    [Space(5)]
    [Tooltip("There are 0 to 4 Audio Sources.")]
    public AudioSource[] audioSources;

    public bool isBossDefeated = false;

    void Awake()
    {
        textCanvas.SetActive(false);
        winTextCanvas.SetActive(false);
    }

    public void RestartButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenuButton()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitButton()
    {
        Application.Quit();
        Debug.Log("Game Quit!");
    }
}
