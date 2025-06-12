using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public int health = 100;
    public ShieldScript shield;

    public Rigidbody camRb;
    public GameObject mainCamera;
    
    public SphereCollider sphereColliderCamera;

    public bool isDead;

    

    public void Update()
    {
        
    }
    public void TakeDamage(int amount)
    {
        if (shield.isActive == false) 
        {
            health -= amount;
            Debug.Log("Player took damage: " + amount);

            if (health <= 0)
            {
                Die();
            }
        }
        else if (shield.isActive == true) 
        {
           shield.ShieldDestroy();
        }
       
    }

    void Die()
    {
        Debug.Log("died");
        StartCoroutine(WaitToRealod());
        isDead = true;
        mainCamera.transform.parent = null;
        sphereColliderCamera.isTrigger = false;
        

        if (camRb != null)
        {
            camRb.isKinematic = false;
            camRb.useGravity = true;

            // Optional: Add a slight force to simulate falling
            camRb.AddForce(Vector3.forward * 1f + Vector3.up * 1f, ForceMode.Impulse);
            camRb.freezeRotation = true;
        }

        // Optional: Unlock cursor for game over screen
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
       
        

    }

    private IEnumerator WaitToRealod()
    {
        yield return new WaitForSeconds(3f);

        string sceneToReload = SceneManager.GetActiveScene().name;
        
        SceneManager.LoadScene(sceneToReload);
        
        
    }


}