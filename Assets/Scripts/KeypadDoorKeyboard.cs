using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadDoorKeyboard : MonoBehaviour, Interactable
{
    [Header("Kod do wpisania (ustawiany w Inspectorze)")]
    [SerializeField] private string correctCode = "1234";

    [Header("UI")]
    [SerializeField] private GameObject keypadUI;
    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private TextMeshProUGUI feedbackText;

    [Header("Drzwi")]
    [SerializeField] private Transform doorTransform;
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 3f, 0f);
    [SerializeField] private float moveSpeed = 2f;
    [SerializeField] private AudioSource doorAudioSource;

    [Header("Raycast")]
    [SerializeField] private float interactionDistance = 3f;
    [SerializeField] private Camera playerCamera; // Kamera gracza

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isUnlocked = false;
    private bool hasPlayedOpenSound = false;

    private void Start()
    {
        closedPosition = doorTransform.position;
        openPosition = closedPosition + openOffset;

        if (keypadUI != null)
            keypadUI.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";

        if (doorAudioSource != null)
            doorAudioSource.playOnAwake = false;

        if (playerCamera == null)
            playerCamera = Camera.main;
    }

    private void Update()
    {
        // Raycast sprawdzaj¹cy, czy gracz patrzy na ten obiekt
        if (!isUnlocked && Input.GetKeyDown(KeyCode.E))
        {
            Ray ray = new Ray(playerCamera.transform.position, playerCamera.transform.forward);
            if (Physics.Raycast(ray, out RaycastHit hit, interactionDistance))
            {
                if (hit.transform == transform)
                {
                    OpenKeypad();
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeypad();
        }

        if (isUnlocked)
        {
            if (!hasPlayedOpenSound && doorAudioSource != null && !doorAudioSource.isPlaying)
            {
                doorAudioSource.Play();
                hasPlayedOpenSound = true;
            }

            doorTransform.position = Vector3.MoveTowards(doorTransform.position, openPosition, moveSpeed * Time.unscaledDeltaTime);
            return;
        }

        if (keypadUI.activeSelf)
        {
            if (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.KeypadEnter))
            {
                SubmitCode();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                CloseKeypad();
            }
        }
    }

    private void OpenKeypad()
    {
        if (keypadUI != null)
        {
            keypadUI.SetActive(true);
            inputField.text = "";
            inputField.ActivateInputField();
            Time.timeScale = 0f;
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CloseKeypad()
    {
        if (keypadUI != null)
            keypadUI.SetActive(false);

        Time.timeScale = 1f;
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;

        if (feedbackText != null)
            feedbackText.text = "";
    }

    public void SubmitCode()
    {
        if (inputField.text == correctCode)
        {
            isUnlocked = true;
            CloseKeypad();
        }
        else
        {
            inputField.text = "";
            inputField.ActivateInputField();

            if (feedbackText != null)
                feedbackText.text = "B³êdny kod!";
        }
    }

    public void Interact()
    {
        OpenKeypad();
    }
}
