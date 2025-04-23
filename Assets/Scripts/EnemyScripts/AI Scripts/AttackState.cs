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
        Debug.Log(losePlayerTimer);
        if (enemy.CanSeePlayer())
        {
            losePlayerTimer = 0f;

            // Continuously update destination
            enemy.Agent.SetDestination(enemy.player.transform.position);
        }
        else
        {
            losePlayerTimer += Time.deltaTime;
            if (losePlayerTimer > 10f)
            {
                stateMachine.ChangeState(new PatrolState());
            }
        }
    }
}
