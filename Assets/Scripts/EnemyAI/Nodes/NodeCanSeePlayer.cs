using UnityEditorInternal;
using UnityEngine;

public class NodeCanSeePlayer : BehaviourNode
{
    // This node checks if the enemy AI can see the player
    private EnemyAIController enemyAI;

    public NodeCanSeePlayer(EnemyAIController enemyAIController)
    {
        enemyAI = enemyAIController;
    }

    public override NodeState Evaluate()
    {
        if (enemyAI.IsPlayerInSight())
        {
            return NodeState.Success;
        }
        return NodeState.Failure;
    }

}