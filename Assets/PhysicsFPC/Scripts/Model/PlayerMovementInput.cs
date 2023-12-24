using UnityEngine;

public class PlayerMovementInput
{
    private const string HorizontalAxis = "Horizontal";
    private const string VerticalAxis = "Vertical";
    private float _horizontalAxisDirection;
    private float _verticalAxisDirection;
    private readonly PlayerMovement _playerMovement;
    private readonly PlayerMovementParameters _movementParameters;
    private readonly PlayerCollider _playerCollider;

    private bool CanInputRun => Input.GetKey(_movementParameters.RunButtonKey) && _movementParameters.RunAvailable;
    private bool CanInputJump => Input.GetKey(_movementParameters.JumpButtonKey) && _movementParameters.JumpAvailable;
    private bool CanInputCrouch => Input.GetKey(_movementParameters.CrouchingButtonKey) && _movementParameters.CrouchAvailable;
    public void InputMove() => _playerMovement.ApplyMoveVelocity(_horizontalAxisDirection, _verticalAxisDirection);

    public PlayerMovementInput(PlayerMovementParameters movementParameters, PlayerMovement playerMovement, PlayerCollider playerCollider)
    {
        _movementParameters = movementParameters;
        _playerMovement = playerMovement;
        _playerCollider = playerCollider;
    }

    public void TryInputJump()
    {
        if (CanInputJump)
            _playerMovement.TryJump();
    }

    public void TryInputCrouch()
    {
        if (CanInputCrouch)
        {
            if (_playerMovement.CanCrouch)
            {
                _playerMovement.TryCrouch();
                _playerCollider.ApplyCrouchParameters();
            }
        }
        else
        {
            _playerMovement.StopCrouching();
            _playerCollider.ApplyWalkParameters();
        }
    }

    public void TryInputRun()
    {
        if (CanInputRun)
            _playerMovement.TryApplyRunSpeed();
        else
            _playerMovement.TryApplyWalkSpeed();
    }

    public void InitializeAxisDirections()
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
}
