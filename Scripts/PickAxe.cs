using UnityEngine;

public class PickAxe : MonoBehaviour
{
    private bool playerInRange = false;
    private PlayerInventory playerInventory;

    private AudioSource audioSource;

    [SerializeField] private AudioClip pickAxeSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        // This checks every single frame. If you are standing 
        // in the green box and hit E, it WILL work now.
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
            Debug.Log("Pickaxe picked up! You can now break rocks.");
            Destroy(gameObject); // Remove the pickaxe from the world
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInRange = true;
            // Get the inventory from the player who just walked over us
            playerInventory = collision.GetComponent<PlayerInventory>();
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