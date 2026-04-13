using UnityEngine;

public class Win : MonoBehaviour
{
    [Space(5)]
    [Header("Audio Sounds")]
    [SerializeField] private AudioClip winSound;

    private GameManager gameManager;
    private PlayerMovement playerMovement;
    private AudioSource audioSource;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    // Start is called before the first frame update
    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && playerMovement.isGameActive)
        {
            audioSource.PlayOneShot(winSound, 8.0f);

            gameManager.audioSources[0].Stop();

            gameManager.winTextCanvas.SetActive(true);

            playerMovement.isGameActive = false;
        }
    }
}
