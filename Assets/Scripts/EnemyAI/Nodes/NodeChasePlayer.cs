using UnityEngine;

public class NodeChasePlayer : BehaviourNode
{
    private EnemyAIController enemyAI;
    public NodeChasePlayer(EnemyAIController enemyAIController)
    {
        enemyAI = enemyAIController;
    }
    public override NodeState Evaluate()
    {
        enemyAI.MoveTo(enemyAI.GetPlayerPosition());
        return NodeState.Running;
    }
}
