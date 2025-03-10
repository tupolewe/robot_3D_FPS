using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public Transform cameraTransform; // Assign your FPS Camera
    public float baseRecoilAmount = 1f; // Starting recoil strength
    public float maxRecoilAmount = 5f; // Maximum recoil strength
    public float recoilSpeed = 10f; // Speed of recoil movement
    public float recoilRecovery = 5f; // Speed of recoil resetting

    public Vector3 currentRotation;
    public Vector3 targetRotation;
    public float currentRecoil; // Tracks increasing recoil
    public float fireRate = 0.1f; // Time between shots
    public float timeSinceLastShot = 0f;

    void Start()
    {
        currentRotation = cameraTransform.localEulerAngles;
    }

    void Update()
    {
        // Time tracking for recoil decay
        timeSinceLastShot += Time.deltaTime;

        // Reduce recoil over time if not shooting
        if (timeSinceLastShot > fireRate)
        {
            currentRecoil = Mathf.Lerp(currentRecoil, baseRecoilAmount, recoilRecovery * Time.deltaTime);
        }

        // Smoothly move towards target rotation
        currentRotation = Vector3.Lerp(currentRotation, targetRotation, recoilSpeed * Time.deltaTime);
        cameraTransform.localRotation = Quaternion.Euler(currentRotation);

        // Slowly reset target rotation
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, recoilRecovery * Time.deltaTime);
    }

    public void ApplyRecoil()
    {
        // Increase recoil the longer we hold fire
        currentRecoil = Mathf.Clamp(currentRecoil + 0.5f, baseRecoilAmount, maxRecoilAmount);

        // Apply recoil with slight randomness
        targetRotation += new Vector3(-currentRecoil, Random.Range(-currentRecoil / 2, currentRecoil / 2), 0);

        // Reset shot timer
        timeSinceLastShot = 0f;
    }
}
