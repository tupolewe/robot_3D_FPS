using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private float losePlayerTimer;

    public override void Enter()
    {
        // Optional: set initial chase destination
        enemy.Agent.SetDestination(enemy.player.transform.position);
    }

    public override void Exit()
    {
        losePlayerTimer = 0f;
    }

    public override void Perform()
    {
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0f;
            enemy.Agent.SetDestination(enemy.player.transform.position);
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 6f)
            {
                stateMachine.ChangeState(new SearchState());
            }
        }
    }
}
