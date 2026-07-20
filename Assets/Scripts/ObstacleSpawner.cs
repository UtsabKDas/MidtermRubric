using UnityEngine;
using System.Collections.Generic;
using System.Collections;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private float spawnInterval = 1f;
    [SerializeField] private float spawnDelay = 1f;
    [SerializeField] private int maxObstacles = 10;

    [SerializeField] private PlayerHealth playerHealth;

    private int currentObstacleCount;
    

    private void Start()
    {
        StartCoroutine(SpawnLoop());
        //InvokeRepeating(nameof(SpawnObstacle), spawnDelay, spawnInterval);
    }

    //private void Update()
    //{
    //    if (GameManager.Instance.IsGameOver)
    //    {
    //        return;
    //    }
    //    if (currentObstacleCount >= maxObstacles && IsInvoking(nameof(SpawnObstacle)))
    //    {
    //        CancelInvoke(nameof(SpawnObstacle));
    //    }
    //}


    private IEnumerator SpawnLoop()
    {
        yield return new WaitForSeconds(spawnDelay);

        while(true)
        {
            if(GameManager.Instance.IsGameOver)
            {
                yield break;
            }
            yield return new WaitUntil(IsCurrentCountLessThanMaxObstacleCount);
            SpawnObstacle();
            yield return new WaitForSeconds(spawnInterval);
        }   
    }

    private bool IsCurrentCountLessThanMaxObstacleCount()
    {
        return currentObstacleCount < maxObstacles;
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
        //if (!GameManager.Instance.IsGameOver)
        //{
        //    Invoke(nameof(SpawnObstacle), spawnInterval);
        //}
    }
}
