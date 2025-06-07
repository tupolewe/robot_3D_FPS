using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float openHeight = 3f; // O ile drzwi maj¹ siê podnieœæ
    public float moveSpeed = 2f;  // Prêdkoœæ przesuwania
    public float closeDelay = 1f; // OpóŸnienie przed zamkniêciem (w sekundach)

    public AudioClip openSound;   // DŸwiêk otwierania
    public AudioClip closeSound;  // DŸwiêk zamykania
    private AudioSource audioSource;

    public Vector3 closedPosition;
    public Vector3 openPosition;
    private bool isOpening = false;
    private bool playerInTrigger = false;
    private float closeTimer = 0f;

    private Rigidbody rb;

    public ParticleSystem sparks1;
    public ParticleSystem sparks2;

    private void Start()
    {
        closedPosition = transform.position;
        openPosition = closedPosition + new Vector3(0f, openHeight, 0f);

        // Ustaw kinematyczne Rigidbody
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }
        rb.isKinematic = true;

        // Dodaj AudioSource, jeœli nie istnieje
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.playOnAwake = false;
    }

    private void Update()
    {
        if (!playerInTrigger)
        {
            // Odliczanie do zamkniêcia drzwi
            if (closeTimer > 0)
            {
                closeTimer -= Time.deltaTime;
                if (closeTimer <= 0)
                {
                    StartClosing();
                }
            }
        }

        Vector3 targetPosition = isOpening ? openPosition : closedPosition;
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            if (!isOpening)
            {
                StartOpening();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            closeTimer = closeDelay; // zaczynamy odliczaæ opóŸnienie
        }
    }

    private void StartOpening()
    {
        isOpening = true;
        PlaySound(openSound);
        sparks1.Play();
        sparks2.Play();
    }

    private void StartClosing()
    {
        isOpening = false;
        sparks1.Play();
        sparks2.Play();
        PlaySound(closeSound);
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.clip = clip;
            audioSource.Play();
        }
    }
}


