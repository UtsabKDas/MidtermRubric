using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Obstacle : MonoBehaviour
{
    [SerializeField] private float minSpeed = 2f;
    [SerializeField] private float maxSpeed = 5f;
    [SerializeField] private int minDamage = 1;
    [SerializeField] private int maxDamage = 5;

    private Rigidbody rb;
    private int damage;
    private ObstacleSpawner spawner;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    private void Start()
    {
        float speed = Random.Range(minSpeed, maxSpeed);
        damage = Random.Range(minDamage, maxDamage + 1);
        Vector3 direction = new Vector3(Random.Range(-1f, 1f), 0f, Random.Range(-1f, 1f)).normalized;
        rb.linearVelocity = direction * speed;
    }

    public void Initialize(ObstacleSpawner obstacleSpawner)
    {
        spawner = obstacleSpawner;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Player"))
        {
            PlayerHealth health = collision.collider.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(damage);
            }
            return;
        }
        DestroyObstacle();
    }

    private void DestroyObstacle()
    {
        if (spawner != null)
        {
            spawner.ObstacleDestroyed();
        }
        Destroy(gameObject);
    }
}
