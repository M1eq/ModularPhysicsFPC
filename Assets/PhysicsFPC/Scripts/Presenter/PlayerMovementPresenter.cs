using UnityEngine;

public class PlayerMovementPresenter : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _movementParameters;
    [SerializeField] private CapsuleCollider _capsuleCollider;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private CameraHandler _cameraHandler;
    [SerializeField] private Transform _playerTransform;
    [SerializeField] private MouseLook _mouseLook;

    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private float _horizontalAxisDirection;
    private float _verticalAxisDirection;

    private void TryInputCrouch()
    {
        if (Input.GetKey(_movementParameters.CrouchingButtonKey) && _movementParameters.CanCrouch)
            _playerMovement.TryCrouch();
        else
            _playerMovement.TryResetHeight();
    }

    private void TryInputRun()
    {
        if (Input.GetKey(_movementParameters.RunButtonKey) && _movementParameters.CanRun)
            _playerMovement.TryApplyRunSpeed();
        else
            _playerMovement.TryApplyWalkSpeed();
    }

    private void TryInputJump()
    {
        if (Input.GetKey(_movementParameters.JumpButtonKey) && _movementParameters.CanJump)
            _playerMovement.TryJump();
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

    private void Awake()
    {
        InitializeCollider();
        _groundChecker.Initialize(_playerTransform, _capsuleCollider.bounds.center, GetCalculatedRaycastLenght());
    }

    private void FixedUpdate()
    {
        InitializeAxisDirections();
        TryInputCrouch();

        _playerMovement.TryCalculateAdjustmentVelocity();
        _playerMovement.TryDeacreaseVerticalSpeed();

        TryInputRun();
        TryInputJump();

        _playerMovement.ApplyMoveVelocity(_horizontalAxisDirection, _verticalAxisDirection);
    }

    private void LateUpdate()
    {
        _cameraHandler.UpdateInterpolation();
        _mouseLook.UpdateLook();
    }

    private void OnEnable() => _cameraHandler.ResetInterpolation();
}
