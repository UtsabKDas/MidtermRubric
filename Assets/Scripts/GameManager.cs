using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    [SerializeField] private ScoreManager scoreManager;
    [SerializeField] private ObstacleSpawner obstacleSpawner;
    [SerializeField] private CoinSpawner coinSpawner;
    [SerializeField] private PlayerController playerController;
    [SerializeField] private HeslerCameraController cameraController;
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
}
