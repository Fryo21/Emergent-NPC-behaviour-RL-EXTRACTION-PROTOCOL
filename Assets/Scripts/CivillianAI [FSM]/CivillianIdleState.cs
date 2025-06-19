using UnityEngine;

public class CivillianIdleState : CivillianAIBaseState
{
    public override void EnterState(CivillianAIController state)
    {
        Debug.Log("Civillian has entered idle state.");
        MoveToNextPoint(state);
    }
    public override void UpdateState(CivillianAIController state)
    {
        Debug.Log("Civillian is in idle state.");
        if (!state.agent.pathPending && state.agent.remainingDistance < 0.5f)
        {
            MoveToNextPoint(state);
        }
    }
    public override void ExitState(CivillianAIController state)
    {
        Debug.Log("Civillian is exiting idle state.");
    }

    private void MoveToNextPoint(CivillianAIController state)
    {
        state.agent.speed = 3.5f;
        int randomIndex = Random.Range(0, state.walkingPoints.Length);
        state.agent.SetDestination(state.walkingPoints[randomIndex].position);
    }
}
