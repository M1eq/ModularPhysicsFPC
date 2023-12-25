using UnityEngine;

abstract public class Checker : MonoBehaviour
{
    private Transform _playerTransfrom;
    private Vector3 _originPoint = Vector3.zero;
    private Vector3 _worldOriginPoint;
    private Vector3 _worldDirection;

    public abstract bool GetHitDetectionResult();

    public void Initialize(Transform playerTransform, Vector3 origin)
    {
        _playerTransfrom = playerTransform;
        _originPoint = _playerTransfrom.InverseTransformPoint(origin);
        InitializeRaycastLength();
    }

    public void ReleaseRaycast()
    {
        ResetHitResult();

        _worldDirection = GetRaycastWorldDirection(_playerTransfrom);
        _worldOriginPoint = _playerTransfrom.TransformPoint(_originPoint);

        CastRay(_worldOriginPoint, _worldDirection);
    }

    protected abstract Vector3 GetRaycastWorldDirection(Transform playerTransform);
    protected abstract void CastRay(Vector3 originPoint, Vector3 direction);
    protected abstract void InitializeRaycastLength();
    protected abstract void ResetHitResult();
}
