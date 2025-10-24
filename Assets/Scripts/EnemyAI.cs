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

        timer += Time.deltaTime;
        if (timer >= speedIncreaseInterval)
        {
            speed += 1f;
            timer = 0f;
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
