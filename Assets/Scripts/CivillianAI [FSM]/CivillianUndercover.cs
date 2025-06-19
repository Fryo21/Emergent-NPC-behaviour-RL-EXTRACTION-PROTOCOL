using UnityEngine;

public class CivillianUndercover : CivillianAIBaseState
{
    public override void EnterState(CivillianAIController state)
    {
        Debug.Log("Civillian has entered undercover state.");
        state.gameObject.AddComponent<EnemyAI>();

        // Destroy everthing about the civillian AI
        GameObject.Destroy(state);
    }

    public override void UpdateState(CivillianAIController state)
    {
     
    }
    public override void ExitState(CivillianAIController state)
    {
        Debug.Log("Civillian is exiting undercover state.");
    }

}