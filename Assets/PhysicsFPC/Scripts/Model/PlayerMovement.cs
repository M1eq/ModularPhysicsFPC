using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private MouseLook _mouseLook;

    private Vector3 _groundAdjustmentVelocity = Vector3.zero;
    private bool _extendedRaycastActivated = true;
    private float currentVerticalSpeed = 0f;
    private float _currentSpeed;
    private bool _crouching;
    private bool _grounded;

    public bool CanCrouch => _grounded && _crouching == false;
    private bool CanApplyWalkSpeed => _crouching == false && _currentSpeed != _movementParameters.WalkSpeed;
    private bool CanApplyRunSpeed => _crouching == false && _currentSpeed != _movementParameters.RunSpeed;
    private bool CanJump => _grounded && _crouching == false;

    public void StopCrouching() => _crouching = false;

    public void TryApplyWalkSpeed()
    {
        if (CanApplyWalkSpeed)
            _currentSpeed = _movementParameters.WalkSpeed;
    }

    public void TryApplyRunSpeed()
    {
        if (CanApplyRunSpeed)
            _currentSpeed = _movementParameters.RunSpeed;
    }

    public void TryCrouch()
    {
        if (CanCrouch)
        {
            _currentSpeed = _movementParameters.CrouchSpeed;
            _crouching = true;
        }
    }

    public void TryJump()
    {
        if (CanJump)
        {
            currentVerticalSpeed = _movementParameters.JumpForce;
            _grounded = false;
        }
    }

    public void ApplyMoveVelocity(float horizontalAxis, float verticalAxis)
    {
        _extendedRaycastActivated = _grounded;

        Vector3 velocity = Vector3.zero;
        velocity += GetCalculatedMovementDirection(horizontalAxis, verticalAxis) * _currentSpeed;
        velocity += transform.up * currentVerticalSpeed;

        _playerRigidbody.velocity = velocity + _groundAdjustmentVelocity;
    }

    public void TryDeacreaseVerticalSpeed()
    {
        if (!_grounded)
        {
            currentVerticalSpeed -= _movementParameters.GravityForce * Time.deltaTime;
        }
        else
        {
            if (currentVerticalSpeed <= 0f)
                currentVerticalSpeed = 0f;
        }
    }

    public void TryCalculateAdjustmentVelocity()
    {
        _groundAdjustmentVelocity = Vector3.zero;

        ReleaseInitializedRaycast();
        _grounded = _groundChecker.GetHitDetectionResult();

        if (_grounded)
        {
            float upperLimit = (_movementParameters.ColliderHeight * (1f - _movementParameters.StepHeightRatio)) * 0.5f;
            float middle = upperLimit + _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;
            float adjustmentMoveDistance = middle - _groundChecker.GetHitDistance();

            _groundAdjustmentVelocity = transform.up * (adjustmentMoveDistance / Time.fixedDeltaTime);
        }
    }

    private void ReleaseInitializedRaycast()
    {
        if (_extendedRaycastActivated)
            _groundChecker.ApplyExtendedRaycastLenght();
        else
            _groundChecker.ApplyBaseRaycastLenght();

        _groundChecker.ReleaseRaycast();
    }

    private Vector3 GetCalculatedMovementDirection(float horizontalAxis, float verticalAxis)
    {
        Vector3 _direction = Vector3.zero;

        _direction += Vector3.ProjectOnPlane(_mouseLook.transform.right, transform.up).normalized * horizontalAxis;
        _direction += Vector3.ProjectOnPlane(_mouseLook.transform.forward, transform.up).normalized * verticalAxis;

        if (_direction.magnitude > 1f)
            _direction.Normalize();

        return _direction;
    }
}
