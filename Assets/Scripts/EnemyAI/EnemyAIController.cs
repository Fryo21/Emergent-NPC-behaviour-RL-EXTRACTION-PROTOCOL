using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAIController : MonoBehaviour
{
    // Store refernces to the player and NavMeshAgent components
    [SerializeField] private Transform player;
    [SerializeField] private NavMeshAgent agent;
    
    [Header("Enemy AI Settings")]
    public float detectionRadius = 20f;
    public float fieldofViewAngle = 110f;

    private BehaviourNode rootNode;

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.LogError("NavMeshAgent component is missing on the enemy.");
        }

        // Initialize the behaviour tree
        BuildBehaviourTree();
    }
    private void Start()
    {
        if (GameObject.FindGameObjectWithTag("Player") != null)
        {

            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        else
        {
            Debug.LogError("Player object with tag 'Player' not found in the scene.");
        }
    }
    // Update is called once per frame
    void Update()
    {
        rootNode.Evaluate();
    }
    private void BuildBehaviourTree()
    {
        // Store references to node states
        var canSeePlayer = new NodeCanSeePlayer(this);
        var chasePlayer = new NodeChasePlayer(this);
        var patrolArea = new NodePatrolArea(this);

        // EnemyAI chase sequence
        var chaseSequence = new SequenceNode(new List<BehaviourNode>
        {
            canSeePlayer,
            chasePlayer
        });

        // Root node of the behaviour tree
        rootNode = new SelectorNode(new List<BehaviourNode>
        {
            chaseSequence,
            patrolArea
        });

    }
    // Check if the player is within the enemy's sight
    public bool IsPlayerInSight()
    {
        Vector3 directionToPlayer = player.position - transform.position;
        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        if (directionToPlayer.magnitude <= detectionRadius && angleToPlayer < fieldofViewAngle * 0.5f)
        {
            Ray ray = new Ray(transform.position + Vector3.up, directionToPlayer.normalized);
            if (Physics.Raycast(ray, out RaycastHit hit, detectionRadius))
            {
                // Check if the raycast hit the player (can use to check for other)
                if (hit.transform.CompareTag("Player"))
                {
                    return true; 
                }
            }
        }
        return false; 
    }
    // Get the player's current position
    public Vector3 GetPlayerPosition()
    {
        return player.position;
    }
    // Move the enemy AI towards a target position
    public void MoveTo(Vector3 targetPosition)
    {
       agent.SetDestination(targetPosition);
    }
}

