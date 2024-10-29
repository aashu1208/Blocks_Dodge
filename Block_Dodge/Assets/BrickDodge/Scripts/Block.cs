using UnityEngine;

public class Block : MonoBehaviour
{
    private Rigidbody2D rb;
    private float initialGravityScale = 0.1f; // Starting gravity scale
    private float maxGravityScale = 3f; // Maximum gravity scale
    private float gravityIncreaseAmount = 0.01f; // Small increment for each step
    private float increaseInterval = 1f; // Time interval (in seconds) between each increase
    private float nextIncreaseTime; // Tracks when to apply the next increase

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = initialGravityScale;
        nextIncreaseTime = Time.time + increaseInterval; // Set the first increase time
    }

    void Update()
    {
        // Gradually increase gravity scale at each time interval
        if (Time.time >= nextIncreaseTime && rb.gravityScale < maxGravityScale)
        {
            rb.gravityScale += gravityIncreaseAmount;
            nextIncreaseTime = Time.time + increaseInterval; // Schedule the next increase
        }

        // Destroy the block if it falls below a certain point and increment score
        if (transform.position.y < -6f)
        {
            Destroy(gameObject);
            FindObjectOfType<GameManager>().SetScore();
        }
    }
}
