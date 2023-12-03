using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private MouseLookParameters _mouseLookParameters;
    [Space(10), SerializeField] private Camera _playerCamera;
    [SerializeField] private Transform _cameraHandler;
    [SerializeField] private PlayerMovement _playerMovement;

    private float _verticalAxis;
    private float _horizontalAxis;
    private float _verticalRotation;
    private float _horizontalRotation;
    private const string MouseAxisX = "Mouse X";
    private const string MouseAxisY = "Mouse Y";

    public void UpdateLook()
    {
        _playerCamera.transform.position = _cameraHandler.position;

        InitializeAxis();
        CalculateRotation();
        RotateLook();
    }

    private void InitializeAxis()
    {
        _horizontalAxis = Input.GetAxis(MouseAxisX) * _mouseLookParameters.HorizontalSenivity * Time.deltaTime;
        _verticalAxis = Input.GetAxis(MouseAxisY) * _mouseLookParameters.VerticalSensivity * Time.deltaTime;
    }

    private void CalculateRotation()
    {
        _horizontalRotation += _horizontalAxis;
        _verticalRotation -= _verticalAxis;
        _verticalRotation = Mathf.Clamp(
            _verticalRotation, _mouseLookParameters.LowerVerticalRotationBorder, _mouseLookParameters.UpperVerticalRotationBorder);
    }

    private void RotateLook()
    {
        transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0);
        _playerMovement.RotateAt(Quaternion.Euler(0, _horizontalRotation, 0));
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
