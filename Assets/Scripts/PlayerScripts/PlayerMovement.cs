using UnityEngine;
using UnityEngine.InputSystem.XR;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float walkSpeed = 6f;
    [SerializeField] private float sprintSpeed = 9f;
    [SerializeField] private float speedSmoothTime = 0.2f;
    private float currentSpeed;
    private float speedSmoothVelocity;

    [SerializeField] Transform playerCamera;
    [SerializeField][Range(0.0f, 0.5f)] float mouseSmoothTime = 0.03f;
    [SerializeField] bool cursorLock = true;
    [SerializeField] float mouseSensitivity;
    [SerializeField] float speed;
    [SerializeField][Range(0.0f, 0.5f)] float moveSmoothTime;
    [SerializeField] float gravity;
    [SerializeField] Transform groundCheck;
    [SerializeField] LayerMask ground;

    public float jumpHeight;
    float velocityY;
    bool isGrounded;

    float cameraCap;
    Vector2 currentMouseDelta;
    Vector2 currentMouseDeltaVelocity;

    CharacterController chController;
    Vector2 currentDir;
    Vector2 currentDirVelocity;
    Vector3 velocity;


    public Vector3 recoilRotation;
    public Vector3 targetRecoilRotation;
    public float recoilRecoverySpeed = 5f;

    public HeadBob headBob;

    void Start()
    {
        chController = GetComponent<CharacterController>();
        Cursor.visible = false;

        if(cursorLock)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = true;
        }
    }

 

    void Update()
    {
        UpdateMouse();
        UpdateMove();
    }

    void UpdateMouse()
    {
        Vector2 targetMouseDelta = new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));

        currentMouseDelta = Vector2.SmoothDamp(currentMouseDelta, targetMouseDelta, ref currentMouseDeltaVelocity, mouseSmoothTime);

        cameraCap -= currentMouseDelta.y * mouseSensitivity;
        cameraCap = Mathf.Clamp(cameraCap, -90.0f, 90.0f);

        
        recoilRotation = Vector3.Lerp(recoilRotation, targetRecoilRotation, recoilRecoverySpeed * Time.deltaTime);

       
        playerCamera.localEulerAngles = Vector3.right * cameraCap + recoilRotation;
        transform.Rotate(Vector3.up * currentMouseDelta.x * mouseSensitivity);

       
        targetRecoilRotation = Vector3.Lerp(targetRecoilRotation, Vector3.zero, recoilRecoverySpeed * Time.deltaTime);
    }

    void UpdateMove()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, 0.2f, ground);

        Vector2 targetDir = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        targetDir.Normalize();

        currentDir = Vector2.SmoothDamp(currentDir, targetDir, ref currentDirVelocity, moveSmoothTime);

        bool isSprinting = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = isSprinting ? sprintSpeed : walkSpeed;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, speedSmoothTime);

        velocityY += gravity * Time.deltaTime;

        Vector3 velocity = (transform.forward * currentDir.y + transform.right * currentDir.x) * currentSpeed + Vector3.up * velocityY;

        chController.Move(velocity * Time.deltaTime);

        //Debug.Log(targetSpeed);

    }

    public void ApplyRecoil(float recoilX, float recoilY)
    {
        targetRecoilRotation += new Vector3(-recoilX, Random.Range(-recoilY, recoilY), 0);
    }
}
