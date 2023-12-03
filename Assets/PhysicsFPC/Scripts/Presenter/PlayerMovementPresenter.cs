using UnityEngine;

public class PlayerMovementPresenter : MonoBehaviour
{
    [SerializeField] private MovementInputParameters _inputParameters;
    [SerializeField] private PlayerMovement _playerMovement;
    [SerializeField] private MouseLook _mouseLook;

    private void Update()
    {
        _mouseLook.UpdateLook();
        _playerMovement.Move();

        if (Input.GetKey(_inputParameters.JumpButton))
            _playerMovement.TryJump();
    }
}
