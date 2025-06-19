using UnityEngine;
using UnityEngine.AI;

public class CivillianAIController : MonoBehaviour
{
    [SerializeField] public NavMeshAgent agent;
    [SerializeField] public Transform[] walkingPoints;
    [SerializeField] public Transform[] safeZones;
    [SerializeField] private Transform playerLocation;
    [SerializeField] public int walkingIndex = 0; // Index of the current walking point
    [SerializeField] private float civillianAudibility = 30f; // Distance within which the civilian can hear gunshots
    [SerializeField] private float enemyPossibility = 0.5f; // 50% chance to convert to enemy AI

    // Reference to the current state of the AI
    private CivillianAIBaseState currentState;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        if (walkingPoints.Length == 0 || safeZones.Length == 0)
        {
            Debug.LogError("Walking points or safe zones are not set in the CivillianAIController.");
            return;
        }
        agent = GetComponent<NavMeshAgent>();
        if (agent == null)
        {
            Debug.Log("NavMeshAgent not assigned, trying to get it from the GameObject.");
        }

        
        // Getthe player location
       // playerLocation = GameObject.FindGameObjectWithTag("Player").transform;

        SwtichState(new CivillianIdleState());

    }

    // Update is called once per frame
    void Update()
    {
        currentState?.UpdateState(this);
    }

    public void SwtichState(CivillianAIBaseState newState)
    {
        // DebugLog
        Debug.Log($"Switching from {currentState?.GetType().Name} to {newState.GetType().Name}");
        currentState?.ExitState(this);
        currentState = newState;
        currentState.EnterState(this);
    }

    public void OnGunShotHeard(Vector3 origin)
    {
        Debug.Log("Gunshot heard! Civillian AI reacting...");
        float distance = Vector3.Distance(transform.position, origin);
        
        if (distance < civillianAudibility)
        {
            float convertChance = Random.value;
            if (convertChance < enemyPossibility)
            {
                // Convert to enemy AI state
                SwtichState(new CivillianUndercover());
            }
            else
            {
                // Flee to safe zone
                SwtichState(new CivillianFlee());
            }
        }
    }

}

