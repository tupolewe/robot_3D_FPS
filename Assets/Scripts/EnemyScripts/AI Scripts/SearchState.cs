using UnityEngine;

public class SearchState : BaseState
{
    private float searchTimer = 0f;
    private float maxSearchTime = 8f;
    private bool reachedSearchPoint = false;
    private int wayPointIndex = 0;

    public override void Enter()
    {
        searchTimer = 0f;
        reachedSearchPoint = false;
        wayPointIndex = 0;

        // Override current path with searchPath
        if (enemy.searchPath != null && enemy.searchPath.waypoints.Count > 0)
        {
            enemy.Agent.SetDestination(enemy.searchPath.waypoints[wayPointIndex].position);
        }
        else
        {
            // If no searchPath, fallback to last known position
            enemy.Agent.SetDestination(enemy.lastKnownPlayerPosition);
        }
    }

    public override void Exit()
    {
        searchTimer = 0f;
        reachedSearchPoint = false;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            stateMachine.ChangeState(new AttackState());
            return;
        }

        // Move between search points
        if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance <= 0.2f)
        {
            reachedSearchPoint = true;
            searchTimer += Time.deltaTime;

            if (searchTimer >= 2f) // Wait at point before moving to next
            {
                wayPointIndex++;

                if (enemy.searchPath != null && enemy.searchPath.waypoints.Count > 0)
                {
                    if (wayPointIndex >= enemy.searchPath.waypoints.Count)
                    {
                        wayPointIndex = 0;
                    }

                    enemy.Agent.SetDestination(enemy.searchPath.waypoints[wayPointIndex].position);
                }

                searchTimer = 0f;
                reachedSearchPoint = false;
            }
        }

        // Total search time maxed out?
        maxSearchTime -= Time.deltaTime;
        if (maxSearchTime <= 0f)
        {
            stateMachine.ChangeState(new PatrolState());
        }
    }
}