using UnityEngine;

[CreateAssetMenu(fileName = "MovementParameters", menuName = "Player/MovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    public float CrouchSpeed => _crouchSpeed;
    public float WalkSpeed => _walkSpeed;
    public float JumpForce => _jumpForce;
    public float RunSpeed => _runSpeed;
    public bool CanJump => _canJump;
    public bool CanRun => _canRun;
    public bool CanCrouching => _canCrouching;
    public float CrouchScaleY => _crouchScaleY;

    [SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _runSpeed = 7.5f;
    [SerializeField] private float _crouchSpeed = 3.5f;
    [SerializeField] private float _jumpForce = 0.3f;
    [SerializeField] private float _crouchScaleY = 0.75f;
    [Space(10), SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canRun = true;
    [SerializeField] private bool _canCrouching = true;
}
