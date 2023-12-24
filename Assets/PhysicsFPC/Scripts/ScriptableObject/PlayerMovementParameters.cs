using UnityEngine;

[CreateAssetMenu(fileName = "MovementParameters", menuName = "Player/MovementParameters")]
public class PlayerMovementParameters : ScriptableObject
{
    public LayerMask WalkableLayer => _walkableLayer;
    public float StepHeightRatio => _stepHeightRatio;
    public float GravityForce => _gravityForce;
    public float CrouchSpeed => _crouchSpeed;
    public float WalkSpeed => _walkSpeed;
    public float JumpForce => _jumpForce;
    public float RunSpeed => _runSpeed;
    public bool RawInput => _rawInput;
    public bool JumpAvailable => _jumpAvailable;
    public bool RunAvailable => _runAvailable;
    public bool CrouchAvailable => _crouchAvailaible;
    public KeyCode JumpButtonKey => _jumpButtonKey;
    public KeyCode RunButtonKey => _runButtonKey;
    public KeyCode CrouchingButtonKey => _crouchingButtonKey;
    public float ColliderHeight => _colliderHeight;
    public float ColliderThickness => _colliderThickness;
    public Vector3 ColliderOffset => _colliderOffset;
    public float ColliderCrouchHeight => _colliderCrouchHeight;
    public float ColliderCrouchThickness => _colliderCrouchThickness;
    public Vector3 ColliderCrouchOffset => _colliderCrouchOffset;
    public float VerticalSensivity => _verticalSensivity;
    public float HorizontalSenivity => _horizontalSensivity;
    public float UpperVerticalRotationBorder => _upperVerticalRotationBorder;
    public float LowerVerticalRotationBorder => _lowerVerticalRotationBorder;
    public float FieldOfView => _fieldOfView;
    public float SmoothSpeed => _smoothSpeed;
    public Vector3 WalkCameraPosition => _walkCameraPosition;
    public Vector3 CrouchCameraPosition => _crouchCameraPosition;

    [Header("Настройки передвижения")]
    [Space(5), SerializeField] private float _walkSpeed = 7;
    [SerializeField] private float _runSpeed = 10f;
    [SerializeField] private float _crouchSpeed = 5f;
    [SerializeField] private float _jumpForce = 12f;
    [SerializeField] private float _gravityForce = 30f;
    [Space(5), SerializeField, Range(0f, 1f)] private float _stepHeightRatio = 0.25f;
    [SerializeField] private LayerMask _walkableLayer = 255;
    [SerializeField] private bool _rawInput = false;
    [Space(5), Header("Настройки коллайдера")]
    [SerializeField] private float _colliderHeight = 2f;
    [SerializeField] private float _colliderThickness = 1f;
    [SerializeField] private Vector3 _colliderOffset = Vector3.zero;
    [Space(10), SerializeField] private float _colliderCrouchHeight;
    [SerializeField] private float _colliderCrouchThickness = 1f;
    [SerializeField] private Vector3 _colliderCrouchOffset = Vector3.zero;
    [Space(5), Header("Настройки камеры")]
    [Space(5), SerializeField] private float _verticalSensivity = 400;
    [SerializeField] private float _horizontalSensivity = 400;
    [SerializeField] private float _upperVerticalRotationBorder = 90;
    [SerializeField] private float _lowerVerticalRotationBorder = -90;
    [SerializeField] private float _smoothSpeed = 20f;
    [Space(5), SerializeField] private Vector3 _walkCameraPosition = new Vector3(0, 0.8f, 0);
    [SerializeField] private Vector3 _crouchCameraPosition = new Vector3(0, 0.3f, 0);
    [Space(5), SerializeField, Range(1, 179)] private float _fieldOfView = 70;
    [Space(5), Header("Настройки ввода")]
    [Space(5), SerializeField] private KeyCode _jumpButtonKey = KeyCode.Space;
    [SerializeField] private KeyCode _runButtonKey = KeyCode.LeftShift;
    [SerializeField] private KeyCode _crouchingButtonKey = KeyCode.LeftControl;
    [Space(5), Header("Настройки опций")]
    [Space(5), SerializeField] private bool _jumpAvailable = true;
    [SerializeField] private bool _runAvailable = true;
    [SerializeField] private bool _crouchAvailaible = true;
}
