using UnityEngine;

public class Snake : MonoBehaviour
{
    private float speed = 10f;
    private float zRotationSpeed = 250f;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        rb.linearVelocity = Vector2.left * speed;
        transform.Rotate(Vector3.forward * zRotationSpeed * Time.deltaTime);
    }
}