using UnityEngine;

public class PickAxe : MonoBehaviour
{
    [Space(5)]
    [Header("Audio Sounds")]
    [Space(10)]
    [SerializeField] private AudioClip pickAxeSound;
    
    private PlayerInventory playerInventory;
    private AudioSource audioSource;

    private bool playerInRange = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PressedKeyE();
    }

    void PressedKeyE()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(pickAxeSound, 1.0f);
            PickUp();
        }
    }

    void PickUp()
    {
        if (playerInventory != null)
        {  
            playerInventory.hasPickaxe = true;
            Destroy(gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInventory = collision.GetComponent<PlayerInventory>();
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = false;
            playerInventory = null;
        }
    }
}