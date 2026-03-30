using UnityEngine;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{
    [Header("Patrol")]
    public Transform pointA;
    public Transform pointB;
    public float patrolSpeed = 2f;

    [Header("Chase")]
    public Transform player;
    public float chaseSpeed = 3.5f;
    public float chaseDistance = 5f;

    [SerializeField] private AudioClip lossingSound;
    [SerializeField] private AudioSource musicSource;

    private Transform targetPoint;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private GameManager gameManager;
    private PlayerMovement playerMovement;

    private AudioSource audioSource;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();

        targetPoint = pointB;
    }

    void Update()
    {
        if (player == null) return;

        if (!playerMovement.isGameActive)
        {
            rb.velocity = Vector2.zero;
            audioSource.PlayOneShot(lossingSound, 0.2f);

            return;
        }

        float distanceToPlayer = Vector2.Distance(transform.position, player.position);

        if (distanceToPlayer <= chaseDistance)
        {
            ChasePlayer();
        }
        else
        {
            Patrol();
        }
    }

    void Patrol()
    {
        // Calculate horizontal direction
        float directionX = targetPoint.position.x - transform.position.x;
            
        // Move enemy (using Sign to ensure consistent speed)
        rb.velocity = new Vector2(Mathf.Sign(directionX) * patrolSpeed, rb.velocity.y);

        // FIX: Only check the horizontal distance (ignore the Y axis)
        if (Mathf.Abs(directionX) < 0.8f) 
        {
            // Switch targets
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }

        Flip(directionX);    
    }

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        Flip(direction.x);
    }

    void Flip(float moveX)
    {
        if (moveX > 0)
            sr.flipX = false;
        else if (moveX < 0)
            sr.flipX = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && playerMovement.isGameActive)
        {
            KillPlayer();
        }
    }

    void KillPlayer()
    {
        musicSource.Stop();

        gameManager.gameOverText.gameObject.SetActive(true);

        gameManager.restartButton.gameObject.SetActive(true);
        gameManager.mainMenuButton.gameObject.SetActive(true);
        gameManager.quitButton.gameObject.SetActive(true);

        playerMovement.isGameActive = false; 
    }
}