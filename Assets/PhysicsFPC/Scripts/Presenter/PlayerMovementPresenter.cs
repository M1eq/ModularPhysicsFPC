using UnityEngine;

public class PlayerMovementPresenter : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private MouseLook _mouseLook;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private float _horizontalAxisDirection;
    private float _verticalAxisDirection;

    private void LateUpdate() => _mouseLook.UpdateLook();

    private void Awake()
    {
        InitializeCollider();
        _groundChecker.Initialize(_playerTransform, _capsuleCollider.bounds.center, GetCalculatedRaycastLenght());
    }

    private void FixedUpdate()
    {
        InitializeAxisDirections();

        if (Input.GetKey(_movementParameters.CrouchingButtonKey) && _movementParameters.CanCrouch)
            _playerMovement.TryCrouch();
        else
            _playerMovement.TryResetHeight();

        _playerMovement.TryCalculateAdjustmentVelocity();
        _playerMovement.TryDeacreaseVerticalSpeed();

        if (Input.GetKey(_movementParameters.RunButtonKey) && _movementParameters.CanRun)
            _playerMovement.TryApplyRunSpeed();
        else
            _playerMovement.TryApplyWalkSpeed();

        if (Input.GetKey(_movementParameters.JumpButtonKey) && _movementParameters.CanJump)
            _playerMovement.TryJump();

        _playerMovement.ApplyMoveVelocity(_horizontalAxisDirection, _verticalAxisDirection);
    }

    private void InitializeAxisDirections()
    {
        if (_movementParameters.RawInput)
        {
            _horizontalAxisDirection = Input.GetAxisRaw(HorizontalAxis);
            _verticalAxisDirection = Input.GetAxisRaw(VerticalAxis);
        }
        else
        {
            _horizontalAxisDirection = Input.GetAxis(HorizontalAxis);
            _verticalAxisDirection = Input.GetAxis(VerticalAxis);
        }
    }

    private void InitializeCollider()
    {
        _capsuleCollider.height = _movementParameters.ColliderHeight;
        _capsuleCollider.center = _movementParameters.ColliderOffset * _movementParameters.ColliderHeight;
        _capsuleCollider.radius = _movementParameters.ColliderThickness / 2f;

        _capsuleCollider.center += new Vector3(0f, _movementParameters.StepHeightRatio * _capsuleCollider.height / 2f, 0f);
        _capsuleCollider.height *= (1f - _movementParameters.StepHeightRatio);

        if (_capsuleCollider.height / 2f < _capsuleCollider.radius)
            _capsuleCollider.radius = _capsuleCollider.height / 2f;
    }

    private float GetCalculatedRaycastLenght()
    {
        float length = 0f;

        length += (_movementParameters.ColliderHeight * (1f - _movementParameters.StepHeightRatio)) * 0.5f;
        length += _movementParameters.ColliderHeight * _movementParameters.StepHeightRatio;

        return length;
    }
}
