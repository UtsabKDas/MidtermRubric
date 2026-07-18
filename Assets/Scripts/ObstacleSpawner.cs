using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int maxObstacles = 10;

    [SerializeField] private PlayerHealth playerHealth;

    private int currentObstacleCount;
    private float spawnTimer;

    private void Start()
    {
        InvokeRepeating(nameof(SpawnObstacle), spawnDelay, spawnInterval);
    }

    private void Update()
    {
        if (playerHealth.IsDead)
        {
            return;
        }
        if (currentObstacleCount >= maxObstacles && IsInvoking(nameof(SpawnObstacle)))
        {
            CancelInvoke(nameof(SpawnObstacle));
        }
    }

    private void SpawnObstacle()
    {
        Transform point = spawnPoints[Random.Range(0, spawnPoints.Length)];
        GameObject obstacle = Instantiate(obstaclePrefab, point.position + Vector3.up, Quaternion.identity);
        obstacle.GetComponent<Obstacle>().Initialize(this);
        currentObstacleCount++;
    }

    public void ObstacleDestroyed()
    {
        currentObstacleCount--;
        Invoke(nameof(SpawnObstacle), spawnInterval);
    }
}
