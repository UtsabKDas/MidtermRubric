using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int coinValue = 1;
    [SerializeField] private float rotateSpeed = 90f;
    [SerializeField] private float bobHeight = 0.5f;
    [SerializeField] private float bobSpeed = 2f;
    [SerializeField] private ScoreManager score;

    private Vector3 startPosition;
    private float minY;
    private float maxY;

    private void Start()
    {
        startPosition = transform.position;
        minY = startPosition.y - bobHeight;
        maxY = startPosition.y + bobHeight;
    }

    private void Update()
    {
        transform.Rotate(Vector3.up, rotateSpeed * Time.deltaTime, Space.World);
        
        if (transform.position.y >= maxY || transform.position.y <= minY)
        {
            bobSpeed = -bobSpeed;
        }
        
        transform.Translate(Vector3.up * bobSpeed * Time.deltaTime, Space.World);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            score.AddScore(coinValue);
            Destroy(gameObject);
        }
    }

    public void SetScoreManager(ScoreManager inputScoreManager)
    {
        score = inputScoreManager;
    }
}
