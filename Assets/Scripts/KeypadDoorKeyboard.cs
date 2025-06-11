using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class KeypadDoorKeyboard : MonoBehaviour, Interactable
{
    [Header("Kod do wpisania (ustawiany w Inspectorze)")]
    [SerializeField] private string correctCode = "1234";

    [Header("UI")]
    [SerializeField] private GameObject keypadUI; // Panel UI
    [SerializeField] private TMP_InputField inputField; // Pole do wpisania kodu
    [SerializeField] private TextMeshProUGUI feedbackText; // Komunikat "b³êdny kod" (opcjonalnie)

    [Header("Drzwi")]
    [SerializeField] private Transform doorTransform;
    [SerializeField] private Vector3 openOffset = new Vector3(0f, 3f, 0f); // Kierunek otwierania drzwi
    [SerializeField] private float moveSpeed = 2f;

    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isUnlocked = false;
    private bool playerInTrigger = false;

    private void Start()
    {
        closedPosition = doorTransform.position;
        openPosition = closedPosition + openOffset;

        if (keypadUI != null)
            keypadUI.SetActive(false);

        if (feedbackText != null)
            feedbackText.text = "";
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CloseKeypad();
        }

        if (isUnlocked)
        {
            doorTransform.position = Vector3.MoveTowards(doorTransform.position, openPosition, moveSpeed * Time.unscaledDeltaTime);
            return;
        }

        if (playerInTrigger && !keypadUI.activeSelf)
        {
            OpenKeypad();
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
            Time.timeScale = 0f; // Pauzuje grê
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
        }
    }

    private void CloseKeypad()
    {
        if (keypadUI != null)
            keypadUI.SetActive(false);

        Time.timeScale = 1f; // Wznawia grê
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

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            CloseKeypad();
        }
    }
}
