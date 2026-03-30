using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5.0f;
    [SerializeField] private float jumpforce = 10.0f;
    [SerializeField] private Camera cam1;
    [SerializeField] private Camera cam2;
    [SerializeField] private Camera cam3;
    [SerializeField] private Camera cam4;
    [SerializeField] private Camera cam5;

    private GameManager gameManager;

    private AudioSource audioSource;

    [SerializeField] private AudioClip jumpSound;


    private bool isOnCam1 = true;
    private bool isOnCam2 = false;
    private bool isOnCam3 = false;
    private bool isOncam4 = false;
    private bool isOncam5 = false;

    public bool isGameActive = true;


    private float lastSwitchTime;
    [SerializeField] private float switchCooldown = 0.3f;

    private bool isOnGround;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.RightArrow) && isGameActive)
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow) && isGameActive)
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }

        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround && isGameActive)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpforce);
            isOnGround = false;

            audioSource.PlayOneShot(jumpSound, 1.0f);
        }
    }


    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            foreach (ContactPoint2D contact in collision.contacts)
            {
                if (contact.normal.y > 0.5f)
                {
                    isOnGround = true;
                }
            }

            
        } 
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ToCam2") && isOnCam1 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam1.gameObject.SetActive(false);
            cam2.gameObject.SetActive(true);
            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = true;
            isOnCam3 = false;
            isOncam4 = false;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam3") && isOnCam2 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam2.gameObject.SetActive(false);
            cam3.gameObject.SetActive(true);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = false;
            isOnCam3 = true;
            isOncam4 = false;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam4") && isOnCam3 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam3.gameObject.SetActive(false);
            cam4.gameObject.SetActive(true);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = false;
            isOnCam3 = false;
            isOncam4 = true;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam5") && isOncam4 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam4.gameObject.SetActive(false);
            cam5.gameObject.SetActive(true);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = false;
            isOnCam3 = false;
            isOncam4 = false;
            isOncam5 = true;
        }

        
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ToCam2") && isOnCam2 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam1.gameObject.SetActive(true);
            cam2.gameObject.SetActive(false);
            lastSwitchTime = Time.time;

            isOnCam1 = true;
            isOnCam2 = false;
            isOnCam3 = false;
            isOncam4 = false;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam3") && isOnCam3 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam2.gameObject.SetActive(true);
            cam3.gameObject.SetActive(false);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = true;
            isOnCam3 = false;
            isOncam4 = false;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam4") && isOncam4 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam3.gameObject.SetActive(true);
            cam4.gameObject.SetActive(false);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = false;
            isOnCam3 = true;
            isOncam4 = false;
            isOncam5 = false;
        }
        else if (other.gameObject.CompareTag("ToCam5") && isOncam5 && Time.time > lastSwitchTime + switchCooldown)
        {
            cam4.gameObject.SetActive(true);
            cam5.gameObject.SetActive(false);

            lastSwitchTime = Time.time;

            isOnCam1 = false;
            isOnCam2 = false;
            isOnCam3 = false;
            isOncam4 = true;
            isOncam5 = false;
        }
    }
}
