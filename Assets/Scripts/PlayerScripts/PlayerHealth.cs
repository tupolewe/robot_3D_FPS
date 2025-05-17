using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public bool shieldActive;

    public void TakeDamage(int amount)
    {
        health -= amount;
        Debug.Log("Player took damage: " + amount);

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        Debug.Log("died");
        
    }

    public void ShieldDie()
    {
        shieldActive = false;
        Debug.Log("shield desctricojdnads");
    }
}