using UnityEngine;

public class ShieldScript : MonoBehaviour
{
    public bool isActive;

    [Header("Audio")]
    public AudioSource src;
    public AudioClip clip;   // Destroy sound
    public AudioClip clip2;  // Activate sound
    public AudioClip clip3;  // Looping active sound
    public AudioClip clip4; // loaded shield sound
    [Header("VFX")]
    public ParticleSystem electricField;
    public ParticleSystem sparks1;
    public ParticleSystem sparks2;

    [Header("Shield Cooldown")]
    [SerializeField] private float cooldownDuration = 5f;
    private float cooldownTimer = 0f;
    public bool isOnCooldown = false;

    private void Start()
    {
        isActive = false;
    }

    private void Update()
    {
        ActiveShield();
        HandleCooldown();
    }

    public void ShieldDestroy()
    {
        sparks1.Play();
        sparks2.Play();
        electricField.Stop();

        src.PlayOneShot(clip);
        src.clip = null;
        src.loop = false;

        isActive = false;
        isOnCooldown = true;
        cooldownTimer = cooldownDuration;
    }

    public void ActiveShield()
    {
        if (Input.GetKeyDown(KeyCode.Q) && !isActive && !isOnCooldown)
        {
            isActive = true;
            electricField.Play();

            src.PlayOneShot(clip2);
            src.clip = clip3;
            src.loop = true;
            src.Play();
        }
    }

    private void HandleCooldown()
    {
        if (!isOnCooldown) return;

        cooldownTimer -= Time.deltaTime;

        if (cooldownTimer <= 0f)
        {
            isOnCooldown = false;
            cooldownTimer = 0f;
            src.PlayOneShot(clip4);
            Debug.Log("Shield is ready!");
        }
        else
        {
            Debug.Log($"Shield cooldown: {cooldownTimer:F1}s");
        }
    }
}