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

    private PlayerMovementInput _playerMovementInput;

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

    private void FixedUpdate()
    {
        _playerMovementInput.InitializeAxisDirections();
        _playerMovementInput.TryInputCrouch();

        _playerMovement.TryCalculateAdjustmentVelocity();
        _playerMovement.TryDeacreaseVerticalSpeed();

        _playerMovementInput.TryInputRun();
        _playerMovementInput.TryInputJump();

        _playerMovementInput.InputMove();
    }

    private void LateUpdate()
    {
        _cameraHandler.UpdateInterpolation();
        _mouseLook.UpdateLook();
    }

    private void Awake()
    {
        _playerMovementInput = new PlayerMovementInput(_movementParameters, _playerMovement);

        InitializeCollider();
        _groundChecker.Initialize(_playerTransform, _capsuleCollider.bounds.center, GetCalculatedRaycastLenght());
    }

    private void OnEnable() => _cameraHandler.ResetInterpolation();
}
