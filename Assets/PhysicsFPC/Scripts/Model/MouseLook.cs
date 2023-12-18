using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private Transform _cameraHandler;
    [SerializeField] private Camera _playerCamera;

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

        transform.rotation = Quaternion.Euler(_verticalRotation, _horizontalRotation, 0);
    }

    private void InitializeAxis()
    {
        _horizontalAxis = Input.GetAxis(MouseAxisX) * _movementParameters.HorizontalSenivity * Time.deltaTime;
        _verticalAxis = Input.GetAxis(MouseAxisY) * _movementParameters.VerticalSensivity * Time.deltaTime;
    }

    private void CalculateRotation()
    {
        _horizontalRotation += _horizontalAxis;
        _verticalRotation -= _verticalAxis;

        _verticalRotation = Mathf.Clamp(
            _verticalRotation, _movementParameters.LowerVerticalRotationBorder, _movementParameters.UpperVerticalRotationBorder);
    }

    private void Start()
    {
        _playerCamera.fieldOfView = _movementParameters.FieldOfView;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }
}
