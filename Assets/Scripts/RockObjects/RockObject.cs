using UnityEngine;

public class RockObject : MonoBehaviour
{
    [Space(5)]
    [Header("Audio Sounds")]
    [SerializeField] private AudioClip rockSound;

    [Space(20)]
    public bool destroyPickaxeOnUse = false;

    private bool playerInRange = false;
    private PlayerInventory playerInv;
    private AudioSource audioSource;

    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        PickUpPickAxe();
    }

    void PickUpPickAxe()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            audioSource.PlayOneShot(rockSound, 1.0f);
            TryBreakRock();
        }
    }

    void TryBreakRock()
    {
        if (playerInv != null && playerInv.hasPickaxe)
        {
            if (destroyPickaxeOnUse)
            {
                playerInv.hasPickaxe = false;
            }

            Destroy(gameObject);
        }
    }

    // Detect when player stands near the rock
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            playerInv = other.GetComponent<PlayerInventory>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }
}