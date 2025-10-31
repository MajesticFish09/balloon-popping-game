using UnityEngine;

public class BalloonGrowth : MonoBehaviour
{
    public float growRate = 0.1f;
    public float maxSize = 3f;

    private GameManager gameManager;
    private BalloonManager balloonManager;

    void Start()
    {
        gameManager = Object.FindFirstObjectByType<GameManager>();
        balloonManager = Object.FindFirstObjectByType<BalloonManager>();

        InvokeRepeating(nameof(Grow), 1f, 1f);
    }

    void Grow()
    {
        transform.localScale += Vector3.one * growRate;

        if (transform.localScale.x >= maxSize)
        {
            CancelInvoke(nameof(Grow));
            // When too big → no points, restart level
            if (gameManager != null)
                gameManager.RestartLevel();
            else
                Debug.LogWarning("GameManager not found!");
            Destroy(gameObject);
        }
    }

    public void Pop()
    {
        CancelInvoke(nameof(Grow));

        if (gameManager != null)
        {
            gameManager.AddScore(transform.localScale.x);
        }

        // Notify BalloonManager that a balloon was popped
        if (balloonManager != null)
        {
            balloonManager.BalloonPopped();
        }

        Destroy(gameObject);
    }
}