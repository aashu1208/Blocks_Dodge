using UnityEngine;

public class Player : MonoBehaviour
{
    public float mapwidth;
    public bool gameover;
    public float moveSpeed = 10f; // Speed at which the player moves

    void Start()
    {
        gameover = false;
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0 && !gameover)
        {
            Touch touch = Input.GetTouch(0);

            // Detect if the touch is on the left or right side of the screen
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                // Check if the touch is on the left half or right half of the screen
                if (touch.position.x < Screen.width / 2)
                {
                    // Touch is on the left side, move the player left
                    MoveLeft();
                }
                else if (touch.position.x > Screen.width / 2)
                {
                    // Touch is on the right side, move the player right
                    MoveRight();
                }
            }
        }
    }

    private void MouseInput()
    {
        if (Input.GetMouseButton(0) && !gameover) // Left mouse button
        {
            // Get the mouse position and convert it to world coordinates
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));

            // Only consider the X-axis for player movement
            float targetX = mousePosition.x;

            // Clamp the target position within map bounds
            targetX = Mathf.Clamp(targetX, -mapwidth, mapwidth);

            // Move the player smoothly to the target position
            Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * Time.deltaTime);
        }
    }

    private void MoveLeft()
    {
        // Move the player left
        if (transform.position.x > -mapwidth)
        {
            transform.Translate(-moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    private void MoveRight()
    {
        // Move the player right
        if (transform.position.x < mapwidth)
        {
            transform.Translate(moveSpeed * Time.deltaTime, 0, 0);
        }
    }

    void FixedUpdate()
    {
        // Unity Editor or standalone desktop platforms input
#if UNITY_EDITOR || UNITY_STANDALONE
        if (!gameover)
        {
            MouseInput(); // Handle mouse input for testing in the editor
        }
#endif

        // Mobile input (Android & iOS)
#if UNITY_ANDROID || UNITY_IOS
        if (!gameover)
        {
            TouchInput(); // Call touch input for both Android and iOS
        }
#endif
    }

    void OnCollisionEnter2D()
    {
        gameover = true;
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        GameObject.Find("GameoverSound").GetComponent<AudioSource>().Play();
        FindObjectOfType<GameManager>().EndGame();
    }
}
