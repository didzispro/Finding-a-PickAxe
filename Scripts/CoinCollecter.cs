using UnityEngine;

public class CoinCollecter : MonoBehaviour
{
    public int coinsCollected = 0;

    public GameObject thingToActivate; // whatever should happen
    public GameObject enemy;

    private AudioSource audioSource;

    [SerializeField] private AudioClip bellSound;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    public void CoinPickedUp()
    {
        audioSource.PlayOneShot(bellSound, 1.0f);
        coinsCollected++;

        if (coinsCollected == 9)
        {
            thingToActivate.SetActive(true);
            Destroy(enemy);
        }
    }
}