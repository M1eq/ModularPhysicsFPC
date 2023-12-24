using UnityEngine;

public class PlayerCollider
{
    private readonly CapsuleCollider _collider;
    private readonly PlayerMovementParameters _movementParameters;
    private readonly CameraHandler _cameraHandler;

    public PlayerCollider(CapsuleCollider collider, PlayerMovementParameters movementParameters, CameraHandler cameraHandler)
    {
        _collider = collider;
        _movementParameters = movementParameters;
        _cameraHandler = cameraHandler;
    }

    public void ApplyWalkParameters()
    {
        _collider.height = _movementParameters.ColliderHeight;
        _collider.center = _movementParameters.ColliderOffset * _movementParameters.ColliderHeight;
        _collider.radius = _movementParameters.ColliderThickness / 2f;

        InitializeWalkColliderSize();
        _cameraHandler.ApplyWalkCameraPosition();
    }

    public void ApplyCrouchParameters()
    {
        _collider.height = _movementParameters.ColliderCrouchHeight;
        _collider.center = _movementParameters.ColliderCrouchOffset * _movementParameters.ColliderCrouchHeight;
        _collider.radius = _movementParameters.ColliderCrouchThickness / 2f;

        _cameraHandler.ApplyCrouchCameraPosition();
    }

    private void InitializeWalkColliderSize()
    {
        _collider.center += new Vector3(0f, _movementParameters.StepHeightRatio * _collider.height / 2f, 0f);
        _collider.height *= (1f - _movementParameters.StepHeightRatio);

        if (_collider.height / 2f < _collider.radius)
            _collider.radius = _collider.height / 2f;
    }
}
