using UnityEngine;

public class ShieldScript : MonoBehaviour
{

    public bool isActive;

    public AudioSource src;
    public AudioClip clip;

    public ParticleSystem sparks1;
    public ParticleSystem sparks2;
    public void ShieldDestroy()
    {
        sparks1.Play();
        sparks2.Play();
        src.PlayOneShot(clip);
        isActive = false;
       
       
    }

    private void Update()
    {
        ActiveShield();
    }

    public void ActiveShield()
    {
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            isActive = true;    
            
        }
    }
}
