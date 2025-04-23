using UnityEngine;

public class SearchState : BaseState
{
    public float searchTimer = 0f;
   public float maxSearchTime = 8f;
    public bool reachedSearchPoint = false;

    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.lastKnownPlayerPosition);
        searchTimer = 0f;
        reachedSearchPoint = false;
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

        if (!reachedSearchPoint)
        {
            if (!enemy.Agent.pathPending && enemy.Agent.remainingDistance <= 0.2f)
            {
                reachedSearchPoint = true;
            }
        }
        else
        {
            searchTimer += Time.deltaTime;


            if (searchTimer >= maxSearchTime)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
}
