using UnityEngine;

public class PlayerMovementPresenter : MonoBehaviour
{
    [SerializeField] private PlayerMovementParameters _inputParameters;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MouseLook _mouseLook;

    private void Update()
    {
        _mouseLook.UpdateLook();

        TryInputRun();
        TryInputCrouching();
        _playerMovement.Move();
        TryInputJump();
    }

    private void TryInputRun()
    {
        if (Input.GetKey(_inputParameters.RunButtonKey) && _playerMovement.Crouching == false)
            _playerMovement.TryRun();
        else
            _playerMovement.NormalizeMoveSpeed();
    }

    private void TryInputCrouching()
    {
        if (Input.GetKey(_inputParameters.CrouchingButtonKey) && _playerMovement.Running == false)
            _playerMovement.TryCrouching();
        else
            _playerMovement.NormalizePlayerHeight();
    }

    private void TryInputJump()
    {
        if (Input.GetKey(_inputParameters.JumpButtonKey) && _playerMovement.Crouching == false)
            _playerMovement.TryJump();
    }
}
