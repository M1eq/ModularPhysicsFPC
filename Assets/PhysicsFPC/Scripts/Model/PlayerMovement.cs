using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public bool Running => _moveSpeed == _movementParameters.RunSpeed;
    public bool Crouching => _crouching;

    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerMovementParameters _movementParameters;

    private bool _crouching;
    private float _moveSpeed;
    private float _startScaleY;
    private float _verticalAxis;
    private float _horizontalAxis;
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidbody;
    private const string VerticalAxis = "Vertical";
    private const string HorizontalAxis = "Horizontal";

    private bool Grounded => _groundChecker.GetGroundCheckResult() == true;
    public void RotateAt(Quaternion angle) => transform.rotation = angle;
    public void NormalizeMoveSpeed() => _moveSpeed = _movementParameters.WalkSpeed;

    public void NormalizePlayerHeight()
    {
        transform.localScale = new Vector3(transform.localScale.x, _startScaleY, transform.localScale.z);
        _crouching = false;
    }

    public void Move()
    {
        InitializeAxis();

        _moveDirection = new Vector3(_horizontalAxis, 0f, _verticalAxis);
        Vector3 moveVelocity = transform.TransformDirection(_moveDirection) * _moveSpeed;
        _playerRigidbody.velocity = new Vector3(moveVelocity.x, _playerRigidbody.velocity.y, moveVelocity.z);
    }

    public void TryRun()
    {
        if (Grounded && _movementParameters.CanRun)
            _moveSpeed = _movementParameters.RunSpeed;
    }

    public void TryJump()
    {
        if (Grounded && _movementParameters.CanJump)
            _playerRigidbody.AddForce(Vector3.up * _movementParameters.JumpForce, ForceMode.Impulse);
    }

    public void TryCrouching()
    {
        if (Grounded && _movementParameters.CanCrouching)
        {
            transform.localScale = new Vector3(transform.localScale.x, _movementParameters.CrouchScaleY, transform.localScale.z);

            if (Crouching == false)
                _playerRigidbody.AddForce(Vector3.down * 5f, ForceMode.Impulse);

            _moveSpeed = _movementParameters.CrouchSpeed;
            _crouching = true;
        }
    }

    private void InitializeAxis()
    {
        _horizontalAxis = Input.GetAxis(HorizontalAxis);
        _verticalAxis = Input.GetAxis(VerticalAxis);
    }

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        _startScaleY = transform.localScale.y;
    }
}
