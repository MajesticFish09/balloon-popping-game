using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public TextMeshProUGUI scoreText;
    private int score;
    private static GameManager instance;

    void Awake()
    {
        // Ensure only one GameManager exists
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        FindScoreText();
        UpdateScoreText();
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Wait a frame then find the score text
        StartCoroutine(FindScoreTextAfterFrame());
    }

    IEnumerator FindScoreTextAfterFrame()
    {
        yield return null; // Wait one frame
        FindScoreText();
        UpdateScoreText();
    }

    void FindScoreText()
    {
        // Look for the score text in the scene
        GameObject scoreTextObj = GameObject.Find("ScoreText");
        if (scoreTextObj != null)
        {
            scoreText = scoreTextObj.GetComponent<TextMeshProUGUI>();
        }

        // If still not found, try to find any TextMeshProUGUI
        if (scoreText == null)
        {
            scoreText = FindObjectOfType<TextMeshProUGUI>();
        }
    }

    public void AddScore(float balloonSize)
    {
        // Give exactly 1 point per balloon popped
        score += 1;
        UpdateScoreText();
    }

    public void RestartLevel()
    {
        Scene current = SceneManager.GetActiveScene();
        SceneManager.LoadScene(current.buildIndex);
    }

    public void LoadNextLevel()
    {
        int nextIndex = SceneManager.GetActiveScene().buildIndex + 1;
        if (nextIndex < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(nextIndex);
        else
            Debug.Log("Game complete! Final Score: " + score);
    }
 
    private void UpdateScoreText()
    {
        if (scoreText != null)
            scoreText.text = "Score: " + score;
        else
            Debug.LogWarning("Score text is null!");
    }

    public void SubtractScore(int pointsToSubtract)
    {
        score -= pointsToSubtract;
        // Ensure score doesn't go below 0
        if (score < 0) score = 0;
        UpdateScoreText();
    }
}