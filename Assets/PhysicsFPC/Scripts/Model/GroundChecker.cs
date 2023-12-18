using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;

    private float _baseRaycastLenght;
    private float _extendedRaycastLenght;
    private float _raycastLength;
    private float _hitDistance;
    private bool _hitDetected;
    private RaycastHit _hit;
    private Transform _playerTransfrom;
    private Vector3 _originPoint = Vector3.zero;
    private const float SafetyDistanceFactor = 1.001f;

    public void ApplyExtendedRaycastLenght() => _raycastLength = _extendedRaycastLenght;
    public void ApplyBaseRaycastLenght() => _raycastLength = _baseRaycastLenght;
    public bool GetHitDetectionResult() => _hitDetected;
    public float GetHitDistance() => _hitDistance;

    public void Initialize(Transform playerTransform, Vector3 origin, float raycastLenght)
    {
        _playerTransfrom = playerTransform;
        _originPoint = _playerTransfrom.InverseTransformPoint(origin);

        _baseRaycastLenght = raycastLenght * SafetyDistanceFactor;
        _extendedRaycastLenght = _baseRaycastLenght + _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;
        _raycastLength = _baseRaycastLenght;
    }

    public void ReleaseRaycast()
    {
        _hitDetected = false;
        _hitDistance = 0f;

        Vector3 worldDirection = -_playerTransfrom.up;
        Vector3 worldOriginPoint = _playerTransfrom.TransformPoint(_originPoint);

        CastRay(worldOriginPoint, worldDirection);
    }

    private void CastRay(Vector3 originPoint, Vector3 direction)
    {
        _hitDetected = Physics.Raycast(originPoint, direction, out _hit, _raycastLength, _movementParameters.WalkableLayer);

        if (_hitDetected)
            _hitDistance = _hit.distance;
    }
}
