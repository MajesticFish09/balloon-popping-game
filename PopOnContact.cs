using UnityEngine;

public class PopOnContact : MonoBehaviour
{
    public AudioClip popSound;
    private AudioSource audioSource;
    private GameManager gameManager;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        gameManager = Object.FindFirstObjectByType<GameManager>();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Balloon"))
        {
            // Play pop sound if available
            if (audioSource != null && popSound != null)
            {
                audioSource.PlayOneShot(popSound);
            }

            // Let the balloon handle its own pop logic
            BalloonGrowth balloon = other.GetComponent<BalloonGrowth>();
            if (balloon != null)
                balloon.Pop();

            // Destroy the pin
            Destroy(gameObject, 0.05f);
        }
        else if (other.CompareTag("Goomba"))
        {
            // Hit a Goomba - lose 2 points and destroy pin
            if (gameManager != null)
            {
                // Use the new SubtractScore method
                gameManager.SubtractScore(2);
            }

            // Destroy the pin immediately
            Destroy(gameObject);

            Debug.Log("Pin hit a Goomba! Lost 2 points.");
        }
    }
}