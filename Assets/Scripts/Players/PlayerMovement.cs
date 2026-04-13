using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private AudioSource audioSource;
    private GameManager gameManager;

    [Space(10)]
    [SerializeField] private float moveSpeed = 5.0f;

    [Space(10)]
    [SerializeField] private float jumpForce = 10.0f;

    [Header("Cameras")]
    [Space(10)]
    [Tooltip("Element 0 = Cam1, Element 1 = Cam2, And to cam5.")]
    [SerializeField] private GameObject[] cameras;

    [Space(20)]
    [SerializeField] private AudioClip jumpSound;

    [Space(20)]
    public bool isGameActive = true;

    private bool isOnGround;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    // Update is called once per frame
    private void Update()
    {
        if (!isGameActive) return;

        Movement();
        HandleJump();
    }

    private void Movement()
    {
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(moveSpeed, rb.velocity.y);
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-moveSpeed, rb.velocity.y);
        }
    }

    private void HandleJump()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && isOnGround)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);

            isOnGround = false;

            audioSource.PlayOneShot(jumpSound, 1.0f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("ToCam1"))
        {
            cameras[0].SetActive(true);
            cameras[1].SetActive(false);
        }
        else if (other.gameObject.CompareTag("ToCam2"))
        {
            cameras[0].SetActive(false);
            cameras[1].SetActive(true);
        }

        else if (other.gameObject.CompareTag("ToCam4"))
        {
            cameras[1].SetActive(false);
            cameras[2].SetActive(true);
        }
        else if (other.gameObject.CompareTag("ToCam3"))
        {
            cameras[1].SetActive(true);
            cameras[2].SetActive(false);  
        }

        else if (other.gameObject.CompareTag("ToCam6"))
        {
            cameras[2].SetActive(false);
            cameras[3].SetActive(true);
        }
        else if (other.gameObject.CompareTag("ToCam5"))
        {
            cameras[2].SetActive(true);
            cameras[3].SetActive(false);  
        }
        
        else if (other.gameObject.CompareTag("ToCam8"))
        {
            cameras[3].SetActive(false);
            cameras[4].SetActive(true);

            if (!gameManager.isBossDefeated)
            {
                gameManager.audioSources[3].Stop();
                gameManager.audioSources[4].Play();
            }
            else 
            {
                if (!gameManager.audioSources[3].isPlaying) 
                {
                    gameManager.audioSources[3].Play();
                }
                gameManager.audioSources[4].Stop();
            }
        } 
        else if (other.gameObject.CompareTag("ToCam7"))
        {
            cameras[3].SetActive(true);
            cameras[4].SetActive(false);

            if (!gameManager.audioSources[3].isPlaying)
            {
                gameManager.audioSources[3].Play();
            }
            
            gameManager.audioSources[4].Stop();
        }
    }
}
