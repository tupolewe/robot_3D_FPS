using UnityEngine;

public class PatrolState : BaseState
{

    public int wayPointIndex;

    public override void Enter()
    {
        if (enemy.path != null && enemy.path.waypoints.Count > 0)
        {
            wayPointIndex = 0;
            enemy.Agent.SetDestination(enemy.path.waypoints[wayPointIndex].position);
        }
    }

    public override void Perform()
    {
        PatrolCycle();
    }

    public override void Exit()
    {
    }

    public void PatrolCycle()
    {
        if (enemy.path == null || enemy.path.waypoints.Count == 0)
            return;

        if (enemy.Agent.remainingDistance < 0.2f && !enemy.Agent.pathPending)
        {
            wayPointIndex++;

            if (wayPointIndex >= enemy.path.waypoints.Count)
            {
                wayPointIndex = 0;
            }

            enemy.Agent.SetDestination(enemy.path.waypoints[wayPointIndex].position);
        }
    }
}
