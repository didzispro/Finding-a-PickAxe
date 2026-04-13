using UnityEngine;

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

    [Header("Music Sources")]
    [Space(5)]
    [Tooltip("There are 0 to 4 Music Sources")]
    [SerializeField] private AudioSource[] musicSources;

    [Space(10)]
    [SerializeField] private AudioClip lossingSound;

    private GameManager gameManager;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private SpriteRenderer sr;
    private Transform targetPoint;
    private AudioSource audioSource;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();

        targetPoint = pointB;
    }

    void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        playerMovement = FindObjectOfType<PlayerMovement>();
    }

    void Update()
    {
        CheckThePlayer();
        DistanceToPlayer();
    }

    void CheckThePlayer()
    {
        if (player == null) return;

        if (!playerMovement.isGameActive)
        {
            rb.velocity = Vector2.zero;

            return;
        }
    }

    void DistanceToPlayer()
    {
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

    void ChasePlayer()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        rb.velocity = new Vector2(direction.x * chaseSpeed, rb.velocity.y);

        Flip(direction.x);
    }

    void Patrol()
    {
        // Calculate horizontal direction
        float directionX = targetPoint.position.x - transform.position.x;
            
        // FIX: Only check the horizontal distance (ignore the Y axis)
        if (Mathf.Abs(directionX) < 0.8f) 
        {
            // Switch targets
            targetPoint = (targetPoint == pointA) ? pointB : pointA;
        }
        
        // Move enemy (using Sign to ensure consistent speed)
        rb.velocity = new Vector2(Mathf.Sign(directionX) * patrolSpeed, rb.velocity.y);

        Flip(directionX);    
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
        foreach (AudioSource audioSource in gameManager.audioSources)
        {
            audioSource.Stop();
        }

        audioSource.PlayOneShot(lossingSound, 1.0f);

        gameManager.textCanvas.SetActive(true);

        playerMovement.isGameActive = false; 
    }
}