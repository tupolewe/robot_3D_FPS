using UnityEngine;
using UnityEngine.UI;

public class Note : MonoBehaviour, Interactable
{
    [Header("UI Elements")]
    public GameObject noteUI;         
    public Image noteImage;           
    public Text noteText;            

    [TextArea]
    public string noteContent;        
    public Sprite noteSprite;         

    private bool isShowing = false;

    void Start()
    {
        if (noteUI != null)
            noteUI.SetActive(false);  
    }

    
    public void Interact()
    {
        if (noteUI == null || noteText == null) return;

        noteUI.SetActive(true);
        noteText.text = noteContent;

        if (noteImage != null && noteSprite != null)
            noteImage.sprite = noteSprite;

        isShowing = true;

        // Optional: freeze player input
        Time.timeScale = 0f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    void Update()
    {
        if (isShowing && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseNote();
        }
    }

    public void CloseNote()
    {
        if (noteUI != null)
            noteUI.SetActive(false);

        isShowing = false;

        // Optional: unfreeze player input
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
