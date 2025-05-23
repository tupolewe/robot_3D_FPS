using UnityEngine;

public class HeadBob : MonoBehaviour
{
    [Header("Configuration")]
    [SerializeField] private bool _enable = true;
    [SerializeField, Range(0, 0.1f)] private float _Amplitude = 0.015f;
    //[SerializeField, Range(0, 30)] private float _frequency = 10.0f;
    [SerializeField] private Transform _camera = null;
    [SerializeField] private Transform _cameraHolder = null;
    public float _frequency;

    private float _toggleSpeed = 3.0f;
    private Vector3 _startPos;
    private CharacterController _controller;
    private float lastSin;

    public AudioSource src;
    public AudioClip clip;
    private void Awake()
    {
        _controller = GetComponent<CharacterController>();
        _startPos = _camera.localPosition;
    }

    void Update()
    {
        if (!_enable) return;
        CheckMotion();
        ResetPosition();
       // _camera.LookAt(FocusTarget());
    }
    private Vector3 FootStepMotion()
    {
        Vector3 pos = Vector3.zero;
        float currentSin = Mathf.Sin(Time.time * _frequency);
        if (currentSin > lastSin && currentSin < 0)
            //src.Play();
        pos.y +=  currentSin * _Amplitude;
        pos.x += Mathf.Cos(Time.time * _frequency / 2) * _Amplitude * 2;
        lastSin = currentSin;
        return pos;
    }
    private void CheckMotion()
    {
        float speed = new Vector3(_controller.velocity.x, 0, _controller.velocity.z).magnitude;
        if (speed < _toggleSpeed) return;
        if (!_controller.isGrounded) return;

        PlayMotion(FootStepMotion());
    }
    private void PlayMotion(Vector3 motion)
    {
        _camera.localPosition += motion;
    }

    private Vector3 FocusTarget()
    {
        Vector3 pos = new Vector3(transform.position.x, transform.position.y + _cameraHolder.localPosition.y, transform.position.z);
        pos += _cameraHolder.forward * 15.0f;
        return pos;
    }
    private void ResetPosition()
    {
        if (_camera.localPosition == _startPos) return;
        _camera.localPosition = Vector3.Lerp(_camera.localPosition, _startPos, 1 * Time.deltaTime);
    }
}

