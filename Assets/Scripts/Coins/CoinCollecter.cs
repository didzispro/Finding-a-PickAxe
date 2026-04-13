using UnityEngine;

public class CoinCollecter : MonoBehaviour
{
    [Space(10)]
    [SerializeField] private int coinsCollected = 0;

    [Space(20)]
    [SerializeField] private GameObject pickaxe;

    [Space(10)]
    [SerializeField] private GameObject enemy;

    [Space(20)]
    [SerializeField] private AudioClip bellSound;

    private AudioSource audioSource;
    private GameManager gameManager;

    void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    public void CoinPickedUp()
    {
        audioSource.PlayOneShot(bellSound, 1.0f);
        coinsCollected++;

        if (coinsCollected == 9)
        {
            gameManager.audioSources[4].Stop();
            gameManager.isBossDefeated = true;
            pickaxe.SetActive(true);
            Destroy(enemy);
        }
    }
}