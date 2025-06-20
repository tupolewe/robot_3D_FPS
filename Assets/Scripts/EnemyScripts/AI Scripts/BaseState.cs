using JetBrains.Annotations;
using UnityEngine;

public abstract class BaseState
{
    public Enemy enemy;
    public StateMachine stateMachine;
    public abstract void Enter();

    public abstract void Perform();

    public abstract void Exit();

    private Animator animator;

    public void PlayWalk()
    {
        enemy.GetComponentInChildren<Animator>().SetBool("patrolState", true);
        enemy.GetComponentInChildren<Animator>().speed = 0.5f;
        enemy.GetComponentInChildren<Animator>().SetBool("isAttacking", false);
        enemy.GetComponentInChildren<Animator>().SetBool("rangeAttack", false);

    }

    public void StopWalk()
    {
        enemy.GetComponentInChildren<Animator>().SetBool("patrolState", false);
        enemy.GetComponentInChildren<Animator>().speed = 0f;
    }

    public void PlayAttack()
    {
        enemy.GetComponentInChildren<Animator>().speed = 1f;
        enemy.GetComponentInChildren<Animator>().SetBool("isAttacking", true);
    }

    public void PlayDeath()
    {
        enemy.GetComponentInChildren<Animator>().SetBool("isDead", true);
    }

    public void StopAttack()
    {
        enemy.GetComponentInChildren<Animator>().speed = 0.5f;
        enemy.GetComponentInChildren<Animator>().SetBool("isAttacking", false);
    }

    public void PlayRangeAttack()
    {
        enemy.GetComponentInChildren<Animator>().SetBool("rangeAttack", true);
    }

    public void DamageAnim()
    {
        Animator anim = enemy.GetComponentInChildren<Animator>();
        anim.SetTrigger("takesDamage?");
        enemy.ResetDamageAnim(anim, 0.6f);
    }

   

}
