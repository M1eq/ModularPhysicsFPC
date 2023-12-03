using UnityEngine;

[CreateAssetMenu(fileName = "MovementInputParameters", menuName = "Player/MovementInputParameters")]
public class MovementInputParameters : ScriptableObject
{
    public KeyCode JumpButtonKey => _jumpButtonKey;
    public KeyCode RunButtonKey => _runButtonKey;
    public KeyCode CrouchingButtonKey => _crouchingButtonKey;

    [SerializeField] private KeyCode _jumpButtonKey = KeyCode.Space;
    [SerializeField] private KeyCode _runButtonKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchingButtonKey = KeyCode.LeftControl;
}
