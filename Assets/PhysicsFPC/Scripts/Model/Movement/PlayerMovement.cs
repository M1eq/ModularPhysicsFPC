using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private RoofChecker _roofChecker;
    [SerializeField] private MouseLook _mouseLook;

    private Vector3 _groundAdjustmentVelocity = Vector3.zero;
    private bool _extendedRaycastActivated = true;
    private float _currentVerticalSpeed = 0f;
    private bool _movingUnderRoof;
    private float _currentSpeed;
    private bool _crouching;
    private bool _grounded;

    public bool CanCrouch => _grounded && _crouching == false;
    public bool CanStopCrouch => _crouching == true && _movingUnderRoof == false;
    private bool CanApplyWalkSpeed => _crouching == false && _currentSpeed != _movementParameters.WalkSpeed;
    private bool CanApplyRunSpeed => _crouching == false && _currentSpeed != _movementParameters.RunSpeed;
    private bool CanJump => _grounded && _crouching == false;

    public void TryStopCrouching()
    {
        if (CanStopCrouch)
            _crouching = false;
    }

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
            _currentVerticalSpeed = _movementParameters.JumpForce;
            _grounded = false;
        }
    }

    public void ApplyMoveVelocity(float horizontalAxis, float verticalAxis)
    {
        _extendedRaycastActivated = _grounded;

        Vector3 velocity = Vector3.zero;
        velocity += GetCalculatedMovementDirection(horizontalAxis, verticalAxis) * _currentSpeed;
        velocity += transform.up * _currentVerticalSpeed;

        _playerRigidbody.velocity = velocity + _groundAdjustmentVelocity;
    }

    public void TryDeacreaseVerticalSpeed()
    {
        if (!_grounded)
        {
            _currentVerticalSpeed -= _movementParameters.GravityForce * Time.deltaTime;
        }
        else
        {
            if (_currentVerticalSpeed <= 0f)
                _currentVerticalSpeed = 0f;
        }
    }

    public void TryCalculateAdjustmentVelocity()
    {
        _groundAdjustmentVelocity = Vector3.zero;

        ReleaseInitializedRaycasts();
        _grounded = _groundChecker.GetHitDetectionResult();
        _movingUnderRoof = _roofChecker.GetHitDetectionResult();

        if (_grounded)
        {
            float upperLimit = (_movementParameters.ColliderHeight * (1f - _movementParameters.StepHeightRatio)) * 0.5f;
            float middle = upperLimit + _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;
            float adjustmentMoveDistance = middle - _groundChecker.GetHitDistance();

            _groundAdjustmentVelocity = transform.up * (adjustmentMoveDistance / Time.fixedDeltaTime);
        }
    }

    private void ReleaseInitializedRaycasts()
    {
        if (_extendedRaycastActivated)
            _groundChecker.ApplyExtendedRaycastLenght();
        else
            _groundChecker.ApplyBaseRaycastLenght();

        _groundChecker.ReleaseRaycast();
        _roofChecker.ReleaseRaycast();
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
