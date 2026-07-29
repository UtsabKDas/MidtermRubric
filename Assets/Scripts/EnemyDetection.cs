using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class EnemyDetection : MonoBehaviour
{
    [SerializeField] private float detectionRadius = 10f;
    [SerializeField] private float viewAngle;
    [SerializeField] private float halfViewAngle = 60f;
    [SerializeField] private LayerMask obstacleLayer;
    [SerializeField] private Transform eyesTransform;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private EnemyPatroller enemyPatroller;

    public bool HasLineOfSightToPlayer { get; private set; }

    private void Awake()
    {
        viewAngle = halfViewAngle * 2f;
        if (playerTransform == null)
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerTransform = player.transform;
            }
        }
    }


    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float distanceToPlayer = Vector3.Distance(eyesTransform.position, playerTransform.position);
        bool isPlayerInRange = distanceToPlayer <= detectionRadius;

        Debug.Log("IsPlayerInRange: " + isPlayerInRange);
        if (isPlayerInRange)
        {
            Vector3 playerAtEyeLevel = new Vector3(playerTransform.position.x, eyesTransform.position.y, playerTransform.position.z);

            Vector3 vectorToPlayerNormalized = (playerAtEyeLevel - eyesTransform.position).normalized; ;
            float angleToPlayer = Vector3.Angle(eyesTransform.forward, vectorToPlayerNormalized);
            bool isPlayerInViewAngle = angleToPlayer <= halfViewAngle;
            Debug.Log("IsPlayerInViewAngle: " + isPlayerInViewAngle);
            Debug.DrawRay(eyesTransform.position, vectorToPlayerNormalized * distanceToPlayer);
            if (isPlayerInViewAngle)
            {
                HasLineOfSightToPlayer = !Physics.Raycast(eyesTransform.position, vectorToPlayerNormalized, distanceToPlayer, obstacleLayer);
                enemyPatroller.SetState(EnemyPatroller.EnemyAIState.Chasing);
            }
            else
            {
                HasLineOfSightToPlayer = false;
                enemyPatroller.SetState(EnemyPatroller.EnemyAIState.Patrolling);
            }
            Debug.Log("HasLineOfSightToPlayer: " + HasLineOfSightToPlayer);
        }
    }

    public Transform GetTargetTransform()
    {
        return playerTransform;
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);

        Gizmos.color = Color.red;
        Vector3 left = Quaternion.Euler(0, -halfViewAngle, 0) * eyesTransform.forward * detectionRadius;
        Vector3 right = Quaternion.Euler(0, halfViewAngle, 0) * eyesTransform.forward * detectionRadius;

        Gizmos.DrawLine(eyesTransform.position, transform.position + left);
        Gizmos.DrawLine(eyesTransform.position, transform.position + right);
    }
}
