using UnityEngine;

public class GroundChecker : Checker
{
    [SerializeField] private PlayerMovementParameters _movementParameters;

    private const float SafetyDistanceFactor = 1.001f;
    private float _extendedRaycastLenght;
    private float _baseRaycastLenght;
    private float _raycastLength;
    private float _hitDistance;
    private bool _hitDetected;
    private RaycastHit _hit;

    public void ApplyExtendedRaycastLenght() => _raycastLength = _extendedRaycastLenght;
    public void ApplyBaseRaycastLenght() => _raycastLength = _baseRaycastLenght;
    public override bool GetHitDetectionResult() => _hitDetected;
    public float GetHitDistance() => _hitDistance;

    protected override void ResetHitResult()
    {
        _hitDetected = false;
        _hitDistance = 0f;
    }

    protected override void CastRay(Vector3 originPoint, Vector3 direction)
    {
        _hitDetected = Physics.Raycast(originPoint, direction, out _hit, _raycastLength, _movementParameters.WalkableLayer);

        if (_hitDetected)
            _hitDistance = _hit.distance;
    }

    protected override void InitializeRaycastLength()
    {
        _baseRaycastLenght = GetBaseRaycastLenght();
        _extendedRaycastLenght = _baseRaycastLenght + _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;
        _raycastLength = _baseRaycastLenght;
    }

    private float GetBaseRaycastLenght()
    {
        float length = 0f;

        length += (_movementParameters.ColliderHeight * (1f - _movementParameters.StepHeightRatio)) * 0.5f;
        length += _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;

        return length * SafetyDistanceFactor;
    }

    protected override Vector3 GetRaycastWorldDirection(Transform playerTransform) => -playerTransform.up;
}
