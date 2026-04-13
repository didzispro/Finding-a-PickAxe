using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private CoinCollecter coinCollecter;



    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Tell platform to go down
            platform.GetComponent<Platform>().GoDown();
            
            // Destroy coin
            Destroy(gameObject);
            coinCollecter.CoinPickedUp();   
        }  
    }
}
