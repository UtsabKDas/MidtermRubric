using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private CoinSpawner coinSpawner;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HeslerCameraController cameraController;
    public HUDManager HUDManager;

    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        if(Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        Instance = this;
     
    }

    private void Start()
    {
        if(scoreManager != null)
        {
            scoreManager = FindAnyObjectByType<ScoreManager>();
        }
        if(obstacleSpawner != null)
        {
            obstacleSpawner = FindAnyObjectByType<ObstacleSpawner>();
        }
        if(coinSpawner != null)
        {
            coinSpawner = FindAnyObjectByType<CoinSpawner>();
        }
        if(playerController != null)
        {
            playerController = FindAnyObjectByType<PlayerController>();
        }
        if(cameraController != null)
        {
            cameraController = FindAnyObjectByType<HeslerCameraController>();
        }
        if(HUDManager != null)
        {
            HUDManager = FindAnyObjectByType<HUDManager>();
        }
        scoreManager.Score = PlayerPrefs.GetInt("Score", 0);
        HUDManager.UpdateScore(scoreManager.Score);

        float playerX = PlayerPrefs.GetFloat("PlayerX", 0);
        float playerY = PlayerPrefs.GetFloat("PlayerY", 0);
        float playerZ = PlayerPrefs.GetFloat("PlayerZ", 0);

        playerController.transform.position = new Vector3(playerX, playerY, playerZ);
        playerController.GetComponent<PlayerHealth>().CurrentHealth = PlayerPrefs.GetInt("PlayerHealth", 0);

    }

    public void OtherOnHealthChangedSubscriber(int x, int y )
    {
        Debug.Log(x + y);
    }

    public void TriggerWin()
    {
        if(IsGameOver)
        {
            return;
        }
        TriggerGameOver();
        Debug.Log("You win");
    }

    private void TriggerGameOver()
    {
        IsGameOver = true;
        coinSpawner.CancelInvoke();
        coinSpawner.enabled = false;
        obstacleSpawner.CancelInvoke();
        obstacleSpawner.enabled = false;
    }

    public void TriggerLose()
    {
        if (IsGameOver)
        {
            return;
        }
        TriggerGameOver();
        Debug.Log("You lose");
    }

    private void OnApplicationQuit()
    {
        Save();
    }

    public void Save()
    {
        PlayerPrefs.SetInt("Score", scoreManager.Score);
        PlayerPrefs.SetInt("PlayerHealth", playerController.GetComponent<PlayerHealth>().CurrentHealth);
        PlayerPrefs.SetFloat("PlayerX", playerController.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", playerController.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", playerController.transform.position.z);
        PlayerPrefs.Save();
    }
}
