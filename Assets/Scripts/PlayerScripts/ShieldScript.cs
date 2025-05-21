using UnityEngine;

public class ShieldScript : MonoBehaviour
{

    public bool isActive;

    public AudioSource src;
    public AudioClip clip;
    public AudioClip clip2;
    public AudioClip clip3;


    public ParticleSystem electricField;
    public ParticleSystem sparks1;
    public ParticleSystem sparks2;
    public void ShieldDestroy()
    {
        sparks1.Play();
        sparks2.Play();
        electricField.Stop();
        src.PlayOneShot(clip);
        src.clip = null;
        src.loop = false;
        isActive = false;
       
       
    }

    public void Start()
    {
        isActive = false;

    }

    private void Update()
    {
        ActiveShield();
    }

    public void ActiveShield()
    {
        if(Input.GetKeyDown(KeyCode.I) && isActive == false) 
        {
            isActive = true;    
            electricField.Play();
            src.PlayOneShot(clip2);
            src.clip = clip3;
            src.loop = true;
            src.Play();
           

        }
    }
}
