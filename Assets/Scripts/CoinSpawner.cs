using UnityEngine;

public class CoinSpawner : MonoBehaviour
{
    [SerializeField] private Coin[] coinPrefabs;
    [SerializeField] private Vector2 areaSize = new Vector2(20f, 20f);
    [SerializeField] private float spawnInterval = 5f;
    [SerializeField] private float spawnDelay = 5f;
    [SerializeField] private float spawnVerticalOffset = 1f;
    [SerializeField] private int initialCoinCount = 5;

    [SerializeField] private PlayerHealth playerHealth;
    [SerializeField] private ScoreManager scoreManager;

    private void Start()
    {
        for (int i = 0; i < initialCoinCount; i++)
        {
            SpawnCoin();
        }
        InvokeRepeating(nameof(SpawnCoin), spawnDelay, spawnInterval);
    }

    private void Update()
    {
        
    }

    public void SpawnCoin()
    {
        float x = Random.Range(-areaSize.x / 2f, areaSize.x / 2f);
        float z = Random.Range(-areaSize.y / 2f, areaSize.y / 2f);
        Vector3 position = new Vector3(x, spawnVerticalOffset, z);

        
        Coin coinToSpawn = coinPrefabs[Random.Range(0, coinPrefabs.Length)];
        Coin spawnedCoin = Instantiate(coinToSpawn, position, coinToSpawn.transform.rotation, transform);
        spawnedCoin.SetScoreManager(scoreManager);
    }
}
