using UnityEngine;
using UnityEngine.AI;

public class EnemyPatroller : MonoBehaviour
{
    public enum EnemyAIState
    {
        Patrolling,
        Chasing
    }

    [SerializeField] private GameObject[] waypoints;
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] EnemyAIState currentState;
    [SerializeField] private int waypointIndex = 0;
    [SerializeField] private float waitTime = 3f;
    [SerializeField] private float startWaitTime;
    [SerializeField] private bool isWaitingAtWaypoint;
    [SerializeField] private EnemyDetection enemyDetection;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        enemyDetection = GetComponent<EnemyDetection>();
    }

    public void SetState(EnemyAIState newState)
    {
        if(currentState != newState)
        {
            currentState = newState;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState == EnemyAIState.Patrolling)
        {
            if (agent.remainingDistance <= agent.stoppingDistance &&
            (!agent.hasPath || agent.velocity.sqrMagnitude < 0.01f))
            {
                if (!isWaitingAtWaypoint)
                {
                    startWaitTime = Time.time;
                    isWaitingAtWaypoint = true;
                }
                if (Time.time - startWaitTime > waitTime)
                {
                    SetNextWaypoint();
                    isWaitingAtWaypoint = false;
                }
            }
        }
        else if (currentState == EnemyAIState.Chasing)
        {
            agent.SetDestination(enemyDetection.GetTargetTransform().position);
        }
    }

    private void SetNextWaypoint()
    {
        agent.SetDestination(waypoints[waypointIndex].transform.position);
        waypointIndex = (waypointIndex + 1) % waypoints.Length;
    }
}
