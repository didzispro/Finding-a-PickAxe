using UnityEngine;

public class RockObject : MonoBehaviour
{
    public bool destroyPickaxeOnUse = false; // Check this in Inspector if you want the tool to break
    private bool playerInRange = false;
    private PlayerInventory playerInv;
    private AudioSource audioSource;

    [SerializeField] private AudioClip rockSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // Check if player is nearby AND presses 'E'
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
            Debug.Log("Rock Destroyed!");

            // Should the pickaxe disappear?
            if (destroyPickaxeOnUse)
            {
                playerInv.hasPickaxe = false;
                Debug.Log("Pickaxe Broke!");
            }

            Destroy(gameObject); // Destroy the rock
        }
        else
        {
            Debug.Log("You need a pickaxe for this!");
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