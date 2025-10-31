using UnityEngine;
using UnityEngine.SceneManagement;

public class BalloonManager : MonoBehaviour
{
    public static BalloonManager instance;

    private int balloonsInLevel;
    private int balloonsPopped;
    private GameManager gameManager;

    void Awake()
    {
        // Make sure only one BalloonManager exists
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
        gameManager = FindObjectOfType<GameManager>();
        CountBalloonsInScene();
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
        // Reset counters when new level loads
        CountBalloonsInScene();
    }

    void CountBalloonsInScene()
    {
        // Find all balloons in the current scene
        BalloonGrowth[] balloons = FindObjectsOfType<BalloonGrowth>();
        balloonsInLevel = balloons.Length;
        balloonsPopped = 0;

        Debug.Log("Found " + balloonsInLevel + " balloons in this level");
    }

    public void BalloonPopped()
    {
        balloonsPopped++;
        Debug.Log("Balloon popped! " + balloonsPopped + "/" + balloonsInLevel + " popped");

        // Check if all balloons are popped
        if (balloonsPopped >= balloonsInLevel)
        {
            Debug.Log("All balloons popped! Loading next level...");
            if (gameManager != null)
            {
                gameManager.LoadNextLevel();
            }
        }
    }
}