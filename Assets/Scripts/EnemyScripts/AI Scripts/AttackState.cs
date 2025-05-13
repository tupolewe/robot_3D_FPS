using Unity.VisualScripting;
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
            float distanceToPlayer = Vector3.Distance(enemy.transform.position, enemy.player.transform.position);

            if(distanceToPlayer > enemy.attackDistance) 
            {
                enemy.Agent.isStopped = false;
                enemy.Agent.SetDestination(enemy.player.transform.position);
            }
            else
            {
                enemy.Agent.isStopped = true;
                Attack();
            }
            
            


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


    public void Attack()
    {
        Debug.Log("Attack");
    }

  
}
