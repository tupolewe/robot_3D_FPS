using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.UI.Image;

public class AttackState : BaseState
{
    private float losePlayerTimer;

    public float attackCooldown = 2f;
    public float lastAttackTime;
    public RangeAttack rangeAttack;

    
   
    public override void Enter()
    {
        enemy.Agent.SetDestination(enemy.player.transform.position);

        // Get RangeAttack component if this is a ranged enemy
        if (enemy.CompareTag("RangeEnemy"))
        {
            rangeAttack = enemy.GetComponent<RangeAttack>();

            if (rangeAttack == null)
                Debug.LogWarning("RangeEnemy has no RangeAttack script attached!");
        }
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
                base.StopAttack();
            }
            else if (enemy.CompareTag("Enemy"))
            {
                enemy.Agent.velocity = new Vector3(0f, 0f, 0f);
                enemy.Agent.isStopped = true;
                Attack();
            }
            else if (enemy.CompareTag("RangeEnemy"))
            {
                enemy.Agent.velocity = Vector3.zero;
                enemy.Agent.isStopped = true;

                if (Time.time >= lastAttackTime + attackCooldown)
                {
                    lastAttackTime = Time.time;
                    rangeAttack.PerformRangeAttack();
                }

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

        base.PlayAttack();
        if (Time.time < lastAttackTime + attackCooldown)
            return;

        lastAttackTime = Time.time;

        float attackRadius = 4f; 
        Vector3 origin = enemy.transform.position + enemy.transform.forward;
        int damage = Random.Range(3, 12);

        Collider[] hits = Physics.OverlapSphere(origin, attackRadius);

       

        foreach (Collider hit in hits)
        {
            if (hit.CompareTag("Player"))
            {
                
                PlayerHealth playerHealth = hit.GetComponent<PlayerHealth>();
                Debug.Log("try to attack");

                if (playerHealth != null)
                {
                    
                        playerHealth.TakeDamage((int)damage);
                        Debug.Log("Enemy hit player!");
                       
                    
                  
                }
                //else if (playerHealth != null && playerHealth.shieldActive)
                //{
                //    playerHealth.ShieldDie();
                //}
            }
            
        }

        Debug.DrawLine(enemy.transform.position, origin, Color.red, 1f);
       


       

    }


   



    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 origin = enemy.transform.position + enemy.transform.forward;
        Gizmos.DrawWireSphere(origin, 3f); // Replace 3f with attackRadius if needed
    }


}
