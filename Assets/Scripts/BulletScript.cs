using UnityEngine;

public class BulletScript : MonoBehaviour
{
    public GameObject bullet;
    public GameObject enemy;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnCollisionEnter(Collision collider)
    {
        //Debug.Log(collider.gameObject);
        enemy = collider.gameObject;

        if (enemy.GetComponent<EnemyScript>() != null)
        {
            Debug.Log("trafienie przeciwmika");
            enemy.SetActive(false);
        }

        //Debug.Log("trafienie");
        Destroy(gameObject);
    }
}
