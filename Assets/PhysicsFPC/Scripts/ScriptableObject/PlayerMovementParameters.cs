using UnityEngine;

[CreateAssetMenu(fileName = "MovementParameters", menuName = "Player/MovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    public float MoveSpeed => _moveSpeed;
    public float JumpForce => _jumpForce;

    [SerializeField] private float _moveSpeed = 10;
    [SerializeField] private float _jumpForce = 0.5f;
}
