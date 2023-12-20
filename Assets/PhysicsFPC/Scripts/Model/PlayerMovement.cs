using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Rigidbody _playerRigidbody;
    [SerializeField] private Transform _cameraTransform;

    private Vector3 _startScale;
    private Vector3 _groundAdjustmentVelocity = Vector3.zero;
    private bool _extendedRaycastActivated = true;
    private float currentVerticalSpeed = 0f;
    private float _currentSpeed;
    private bool _crouching;
    private bool _grounded;

    public void TryApplyWalkSpeed()
    {
        if (_crouching == false && _currentSpeed != _movementParameters.WalkSpeed)
            _currentSpeed = _movementParameters.WalkSpeed;
    }

    public void TryApplyRunSpeed()
    {
        if (_crouching == false && _currentSpeed != _movementParameters.RunSpeed)
            _currentSpeed = _movementParameters.RunSpeed;
    }

    public void TryResetHeight()
    {
        if (transform.localScale != _startScale)
        {
            transform.localScale = _startScale;
            _crouching = false;
        }
    }

    public void TryCrouch()
    {
        if (_grounded && transform.localScale.y != _movementParameters.CrouchScaleY)
        {
            transform.localScale = new Vector3(transform.localScale.x, _movementParameters.CrouchScaleY, transform.localScale.z);
            _currentSpeed = _movementParameters.CrouchSpeed;
            _crouching = true;
        }
    }

    public void TryJump()
    {
        if (_grounded && _crouching == false)
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

        _direction += Vector3.ProjectOnPlane(_cameraTransform.right, transform.up).normalized * horizontalAxis;
        _direction += Vector3.ProjectOnPlane(_cameraTransform.forward, transform.up).normalized * verticalAxis;

        if (_direction.magnitude > 1f)
            _direction.Normalize();

        return _direction;
    }

    private void Awake() => _startScale = transform.localScale;
}
