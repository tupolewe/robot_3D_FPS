using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AttackState : BaseState
{
    private float losePlayerTimer;

    public float attackCooldown = 2f;
    public float lastAttackTime;

    

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
                enemy.Agent.velocity = new Vector3(0f, 0f, 0f);
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
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        float attackRadius = 3f; 
        Vector3 origin = enemy.transform.position + enemy.transform.forward;
        int damage = Random.Range(3, 12);

        Collider[] hits = Physics.OverlapSphere(origin, attackRadius);

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                

                if (playerHealth != null && playerHealth.shieldActive == false)
                {
                    
                        playerHealth.TakeDamage((int)damage);
                        Debug.Log("Enemy hit player!");
                       
                    
                  
                }
                else if (playerHealth != null && playerHealth.shieldActive)
                {
                    playerHealth.ShieldDie();
                }
            }
            
        }

        Debug.DrawLine(enemy.transform.position, origin, Color.red, 1f);
       
    }

  
}
