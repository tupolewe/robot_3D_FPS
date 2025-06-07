using UnityEngine;

public class SlidingDoor : MonoBehaviour
{
    public float openHeight = 3f; // O ile drzwi maj� si� podnie��
    public float moveSpeed = 2f;  // Pr�dko�� przesuwania
    public float closeDelay = 1f; // Op�nienie przed zamkni�ciem (w sekundach)

    public AudioClip openSound;   // D�wi�k otwierania
    public AudioClip closeSound;  // D�wi�k zamykania
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

        // Dodaj AudioSource, je�li nie istnieje
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
            // Odliczanie do zamkni�cia drzwi
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
            closeTimer = closeDelay; // zaczynamy odlicza� op�nienie
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


