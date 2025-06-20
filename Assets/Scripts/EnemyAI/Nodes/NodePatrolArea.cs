using Unity.Hierarchy;
using UnityEngine;

public class NodePatrolArea : BehaviourNode
{
    private EnemyAIController enemyAI;
    private Vector3 patrolPoint;
    private bool pointSet = false;
    private float patrolRadius = 10f; 
    public NodePatrolArea(EnemyAIController enemyAIController)
    {
        enemyAI = enemyAIController;
    }
    public override NodeState Evaluate()
    {
        
        Debug.Log("Patrolling the area...");
        if (!pointSet)
        {
            Vector3 patrolOffset = new Vector3(Random.Range(-patrolRadius, patrolRadius), 0, Random.Range(-patrolRadius, patrolRadius));
            patrolPoint = enemyAI.transform.position + patrolOffset;

            enemyAI.MoveTo(patrolPoint);
            pointSet = true;
        }
        float distanceToPatrolPoint = Vector3.Distance(enemyAI.transform.position, patrolPoint);
        if (distanceToPatrolPoint < 2f)
        {
            pointSet = false;
        }
        // Return Running state to indicate that the patrol is ongoing
        return NodeState.Running;
    }
}
