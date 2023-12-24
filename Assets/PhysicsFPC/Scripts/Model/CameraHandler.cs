using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private Transform _playerTransform;

    private Vector3 _currentPosition;
    private Quaternion _currentRotation;
    private Vector3 _localPositionOffset;

    public void ApplyCrouchCameraPosition() => _localPositionOffset = _movementParameters.CrouchCameraPosition;
    public void ApplyWalkCameraPosition() => _localPositionOffset = _movementParameters.WalkCameraPosition;

    public void ResetInterpolation()
    {
        _currentRotation = _playerTransform.rotation;

        Vector3 _offset = transform.localToWorldMatrix * _localPositionOffset;
        _currentPosition = _playerTransform.position + _offset;
    }

    public void UpdateInterpolation()
    {
        _currentRotation = GetSmoothRotation(_currentRotation, _playerTransform.rotation, _movementParameters.SmoothSpeed);
        transform.rotation = _currentRotation;

        _currentPosition = GetSmoothPosition(_currentPosition, _playerTransform.position, _movementParameters.SmoothSpeed);
        transform.position = _currentPosition;
    }

    private Vector3 GetSmoothPosition(Vector3 startPosition, Vector3 targetPosition, float smoothTime)
    {
        Vector3 offset = transform.localToWorldMatrix * _localPositionOffset;

        targetPosition += offset;
        return Vector3.Lerp(startPosition, targetPosition, Time.deltaTime * smoothTime);
    }

    private Quaternion GetSmoothRotation(Quaternion currentRotation, Quaternion targetRotation, float smoothSpeed) =>
        Quaternion.Slerp(currentRotation, targetRotation, Time.deltaTime * smoothSpeed);

    private void Awake()
    {
        _currentRotation = transform.rotation;
        _currentPosition = transform.position;
        _localPositionOffset = transform.localPosition;
    }

    private void OnEnable() => ResetInterpolation();
}
