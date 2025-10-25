using UnityEngine;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    private Rigidbody2D erb;
    private Animator ea;
    public Transform player;
    public float speed = 3f;
    private float timer = 0f;
    private float speedIncreaseInterval = 3f;

    [Header("Obstacle Detection")]
    [SerializeField] private float jumpForce = 8f;
    [SerializeField] private float detectionDistance = 2f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private bool isGrounded = false;

    void Start()
    {
        erb = GetComponent<Rigidbody2D>();
        ea = GetComponent<Animator>();
    }

    void Update()
    {
        if (player != null)
        {
            Vector3 direction = (player.position - transform.position).normalized;
            transform.Translate(direction * speed * Time.deltaTime);
        }

        CheckForObstacles();

        timer += Time.deltaTime;
        if (timer >= speedIncreaseInterval)
        {
            speed += 0.5f;
            timer = 0f;
        }
    }

    void CheckForObstacles()
    {
        RaycastHit2D hit = Physics2D.Raycast(
            transform.position,
            Vector2.right * Mathf.Sign(transform.localScale.x),
            detectionDistance,
            obstacleLayer
        );

        if (hit.collider != null && hit.collider.CompareTag("Obstacle") && isGrounded)
        {
            JumpOverObstacle();
        }
    }

    void JumpOverObstacle()
    {
        if (isGrounded)
        {
            erb.AddForce(new Vector2(0f, jumpForce), ForceMode2D.Impulse);
            isGrounded = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Snake"))
        {
            Destroy(other.gameObject);
            StartCoroutine(DecraseSpeed(2f));
        }
    }

    IEnumerator DecraseSpeed(float delay)
    {
        float originalSpeed = speed;
        speed = Mathf.Max(2f, speed - 2f);
        yield return new WaitForSeconds(delay);
        speed = originalSpeed;
    }

}