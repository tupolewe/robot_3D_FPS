using UnityEngine;

public class CameraRecoil : MonoBehaviour
{
    public Transform cameraTransform; // Assign your camera (or player object)
    public float recoilAmount = 2f; // How much the camera moves
    public float recoilSpeed = 10f; // How fast it returns
    public float recoilRecovery = 5f; // How fast it recovers

    private Vector3 originalRotation;
    private Vector3 targetRotation;

    void Start()
    {
        originalRotation = cameraTransform.localEulerAngles;
    }

    void Update()
    {
        // Smoothly lerp recoil effect back to original position
        targetRotation = Vector3.Lerp(targetRotation, Vector3.zero, recoilRecovery * Time.deltaTime);
        cameraTransform.localEulerAngles = originalRotation + targetRotation;
    }

    public void ApplyRecoil()
    {
        // Apply recoil in a random upward direction
        targetRotation += new Vector3(-recoilAmount, Random.Range(-recoilAmount / 2, recoilAmount / 2), 0);
    }
}
