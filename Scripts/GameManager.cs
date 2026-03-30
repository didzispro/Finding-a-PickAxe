using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI youWinText;
    public TextMeshProUGUI gameOverText;

    public Button restartButton;
    public Button mainMenuButton;
    public Button quitButton;

    public AudioSource musicSource;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
