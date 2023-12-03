using UnityEngine;

[CreateAssetMenu(fileName = "MovementInputParameters", menuName = "Player/MovementInputParameters")]
public class MovementInputParameters : ScriptableObject
{
    public KeyCode JumpButton => _jumpButton;

    [SerializeField] private KeyCode _jumpButton = KeyCode.Space;
}
