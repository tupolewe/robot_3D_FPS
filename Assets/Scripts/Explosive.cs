using UnityEngine;

public class Explosive : MonoBehaviour, IDamageable
{
    [SerializeField] private int health = 3;
    [SerializeField] private int explosionDamage;
    [SerializeField] private float explosionRadius;
    [SerializeField] LayerMask damageLayers;

    public ParticleSystem explosionVFX;

    private bool exploded;

    public void TakeDamage(int damage)
    {
        if (exploded)
        { return; }

        health -= damage;

        Debug.Log($"Barrel HP: {health}");

        if (health <= 0)
        {
            Explode();
        }
    }

    private void Explode()
    {
        if(exploded)
        { return; }

        exploded = true;

        Debug.Log("BOOM!");
        

        Collider[] colliders = Physics.OverlapSphere(transform.position, explosionRadius);

        foreach (Collider collider in colliders)
        {
            Debug.Log(collider.gameObject.name);
            IDamageable damageable = collider.GetComponent<IDamageable>();
            if (damageable != null)
            {
                
                damageable.TakeDamage(explosionDamage);
                
            }
        }

        explosionVFX.gameObject.SetActive(true);
        explosionVFX.Play();
        Destroy(gameObject);




    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawSphere(transform.position, explosionRadius);
        Debug.Log("zaznacznoe");
    }
}