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
    private PlayerCollider _playerCollider;

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
        _playerCollider = new PlayerCollider(_capsuleCollider, _movementParameters, _cameraHandler);
        _playerMovementInput = new PlayerMovementInput(_movementParameters, _playerMovement, _playerCollider);

        _playerCollider.ApplyWalkParameters();
        _groundChecker.Initialize(_playerTransform, _capsuleCollider.bounds.center);
    }
}
