using UnityEngine;

public class RoofChecker : Checker
{
    [SerializeField] private PlayerMovementParameters _movementParameters;

    private float _raycastLength;
    private bool _hitDetected;

    public override bool GetHitDetectionResult() => _hitDetected;

    protected override void CastRay(Vector3 originPoint, Vector3 direction) => 
        _hitDetected = Physics.Raycast(originPoint, direction, _raycastLength, _movementParameters.WalkableLayer);
 
    protected override Vector3 GetRaycastWorldDirection(Transform playerTransform) => playerTransform.up;
    protected override void InitializeRaycastLength() => _raycastLength = _movementParameters.RoofRaycastLenght;
    protected override void ResetHitResult() => _hitDetected = false;

    private void OnDrawGizmos() => Gizmos.DrawRay(transform.position, transform.up * _raycastLength);
}