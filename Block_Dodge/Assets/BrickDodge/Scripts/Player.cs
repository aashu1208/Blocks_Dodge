using UnityEngine;

public class Player : MonoBehaviour
{
    public float mapwidth;
    public bool gameover;
    public float moveSpeed = 10f; // Base speed
    public float speedIncreaseRate = 0.1f; // Speed increase per second
    private float currentSpeedMultiplier = 1f;

    void Start()
    {
        gameover = false;
    }

    void FixedUpdate()
    {
        if (!gameover)
        {
            currentSpeedMultiplier += speedIncreaseRate * Time.deltaTime;

#if UNITY_EDITOR || UNITY_STANDALONE
            MouseInput(); // Handle mouse input for testing in the editor
#endif

#if UNITY_ANDROID || UNITY_IOS
            TouchInput(); // Call touch input for both Android and iOS
#endif
        }
    }

    private void TouchInput()
    {
        if (Input.touchCount > 0 && !gameover)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Stationary || touch.phase == TouchPhase.Moved)
            {
                if (touch.position.x < Screen.width / 2) MoveLeft();
                else if (touch.position.x > Screen.width / 2) MoveRight();
            }
        }
    }

    private void MouseInput()
    {
        if (Input.GetMouseButton(0) && !gameover)
        {
            Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
            float targetX = Mathf.Clamp(mousePosition.x, -mapwidth, mapwidth);
            Vector3 newPosition = new Vector3(targetX, transform.position.y, transform.position.z);
            transform.position = Vector3.Lerp(transform.position, newPosition, moveSpeed * currentSpeedMultiplier * Time.deltaTime);
        }
    }

    private void MoveLeft() { if (transform.position.x > -mapwidth) transform.Translate(-moveSpeed * currentSpeedMultiplier * Time.deltaTime, 0, 0); }
    private void MoveRight() { if (transform.position.x < mapwidth) transform.Translate(moveSpeed * currentSpeedMultiplier * Time.deltaTime, 0, 0); }

    void OnCollisionEnter2D()
    {
        gameover = true;
        FindObjectOfType<GameManager>().GetComponent<AudioSource>().Stop();
        GameObject.Find("GameoverSound").GetComponent<AudioSource>().Play();
        FindObjectOfType<GameManager>().EndGame();
    }
}
