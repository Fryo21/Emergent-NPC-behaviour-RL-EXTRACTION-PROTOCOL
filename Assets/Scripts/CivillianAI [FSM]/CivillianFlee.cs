using UnityEngine;

public class CivillianFlee : CivillianAIBaseState
{
    
    public override void EnterState(CivillianAIController state)
    {
        state.agent.speed = 5.0f;

        Transform zonePosition = GetClosestSafeZone(state);

        state.agent.SetDestination(zonePosition.position);

        Debug.Log("Civillian has entered flee state.");        
    }
    public override void UpdateState(CivillianAIController state)
    {
        // if civillian is close to safe zone, out of sight then delete the civillian
        if (HasReachedSafeZone(state))
        { 
            Debug.Log("Civillian has reached the safe zone and is now safe.");
            Object.Destroy(state.gameObject);
        }
    }
    public override void ExitState(CivillianAIController state)
    {
        Debug.Log("Civillian is exiting flee state.");
    }
    private Transform GetClosestSafeZone(CivillianAIController state)
    {
        Transform closestSafeZone = null;
        float closestDistance = Mathf.Infinity;

        foreach (Transform zone in state.safeZones)
        {
            float distance = Vector3.Distance(state.transform.position, zone.position);
            if(distance < closestDistance)
            {
                closestDistance = distance;
                closestSafeZone = zone;
            }
        }
        return closestSafeZone;
    }
    private bool HasReachedSafeZone(CivillianAIController state)
    {
        float distanceToSafeZone = Vector3.Distance(state.transform.position, GetClosestSafeZone(state).position);
        if (distanceToSafeZone < 2f)
        { 
            return true; 
        }
        return false;
    }
}
