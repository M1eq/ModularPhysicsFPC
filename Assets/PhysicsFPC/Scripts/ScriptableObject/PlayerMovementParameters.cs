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
    public float CrouchDownForce => _crouchDownForce;
    public KeyCode JumpButtonKey => _jumpButtonKey;
    public KeyCode RunButtonKey => _runButtonKey;
    public KeyCode CrouchingButtonKey => _crouchingButtonKey;
    public float VerticalSensivity => _verticalSensivity;
    public float HorizontalSenivity => _horizontalSensivity;
    public float UpperVerticalRotationBorder => _upperVerticalRotationBorder;
    public float LowerVerticalRotationBorder => _lowerVerticalRotationBorder;

    [Header("Настройки передвижения")]
    [Space(5), SerializeField] private float _walkSpeed = 5;
    [SerializeField] private float _runSpeed = 7.5f;
    [SerializeField] private float _crouchSpeed = 3.5f;
    [SerializeField] private float _jumpForce = 0.3f;
    [SerializeField] private float _crouchScaleY = 0.75f;
    [SerializeField] private float _crouchDownForce = 5f;
    [Space(5), Header("Настройки камеры")]
    [Space(5), SerializeField] private float _verticalSensivity = 400;
    [SerializeField] private float _horizontalSensivity = 400;
    [SerializeField] public float _upperVerticalRotationBorder = 90;
    [SerializeField] public float _lowerVerticalRotationBorder = -90;
    [Space(5), Header("Настройки ввода")]
    [Space(5), SerializeField] private KeyCode _jumpButtonKey = KeyCode.Space;
    [SerializeField] private KeyCode _runButtonKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchingButtonKey = KeyCode.LeftControl;
    [Space(5), Header("Настройки опций")]
    [Space(5), SerializeField] private bool _canJump = true;
    [SerializeField] private bool _canRun = true;
    [SerializeField] private bool _canCrouching = true;
}
