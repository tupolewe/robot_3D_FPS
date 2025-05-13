using UnityEngine;

public class EnemyHearing : MonoBehaviour
{
    public Enemy enemy;

    private void Start()
    {
       
    }
    public void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player"))
        {
            Debug.Log("heard");
            enemy.hasHeardPlayer = true;
            enemy.lastKnownPlayerPosition = enemy.player.transform.position;

        }
    }
}
