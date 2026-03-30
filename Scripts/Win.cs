using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Win : MonoBehaviour
{
    private GameManager gameManager;
    private PlayerMovement playerMovement;

    private AudioSource audioSource;

    [SerializeField] private AudioClip winSound;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerMovement.isGameActive)
        {
            audioSource.PlayOneShot(winSound, 0.2f);

            gameManager.musicSource.Stop();

            gameManager.youWinText.gameObject.SetActive(true);

            gameManager.restartButton.gameObject.SetActive(true);
            gameManager.mainMenuButton.gameObject.SetActive(true);
            gameManager.quitButton.gameObject.SetActive(true);

            playerMovement.isGameActive = false;
        }
    }

    
}
