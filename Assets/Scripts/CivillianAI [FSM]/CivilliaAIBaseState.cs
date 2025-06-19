using UnityEngine;

public abstract class CivillianAIBaseState
{
    public abstract void EnterState(CivillianAIController state);
    public abstract void UpdateState(CivillianAIController state);
    public abstract void ExitState(CivillianAIController state);

}
