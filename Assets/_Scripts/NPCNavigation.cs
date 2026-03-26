using UnityEngine;
using UnityEngine.AI;

public class NPCPatrol : MonoBehaviour
{
    public Transform[] waypoints;
    private int currentWaypoint = 0;
    private NavMeshAgent agent;
    public Animator anim;
    public Transform player;
    public float detectionRange = 5f;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        GoToNextWaypoint();
        anim.SetBool("Walk", true);
    }

    void Update()
    {
    float distance = Vector3.Distance(transform.position, player.position);

    if (distance < detectionRange)
    {
        agent.SetDestination(player.position);
    }
    else if (!agent.pathPending && agent.remainingDistance < 0.5f)
    {
        GoToNextWaypoint();
    }
    }

    void GoToNextWaypoint()
    {
        if (waypoints.Length == 0) return;

        agent.destination = waypoints[currentWaypoint].position;
        currentWaypoint = (currentWaypoint + 1) % waypoints.Length;
    }
}
