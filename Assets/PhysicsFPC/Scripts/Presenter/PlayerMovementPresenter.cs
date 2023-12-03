using UnityEngine;

public class PlayerMovementPresenter : MonoBehaviour
{
    [SerializeField] private MovementInputParameters _inputParameters;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MouseLook _mouseLook;

    private void Update()
    {
        _mouseLook.UpdateLook();

        if (Input.GetKey(_inputParameters.RunButtonKey) && _playerMovement.Crouching == false)
            _playerMovement.TryRun();
        else
            _playerMovement.NormalizeMoveSpeed();

        if (Input.GetKey(_inputParameters.CrouchingButtonKey) && _playerMovement.Running == false)
            _playerMovement.TryCrouching();
        else
            _playerMovement.NormalizePlayerHeight();

        _playerMovement.Move();

        if (Input.GetKey(_inputParameters.JumpButtonKey) && _playerMovement.Crouching == false)
            _playerMovement.TryJump();
    }
}
