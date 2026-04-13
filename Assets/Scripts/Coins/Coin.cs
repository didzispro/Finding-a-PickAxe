using UnityEngine;

public class Coin : MonoBehaviour
{
    [Space(5)]
    [SerializeField] private GameObject platform;
    
    [Space(10)]
    [SerializeField] private CoinCollecter coinCollecter;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            platform.GetComponent<Platform>().GoDown();
            
            Destroy(gameObject);
            coinCollecter.CoinPickedUp();   
        }  
    }
}
