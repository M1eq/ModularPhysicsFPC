using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    public bool Running => _moveSpeed == _movementParameters.RunSpeed;
    public bool Crouching => _crouching;

    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private Transform _playerOrientation;
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
    private bool Sloped => _groundChecker.GetSlopeGroundCheckResult() == true;
    private bool CanRun => Grounded && _movementParameters.CanRun || Sloped && _movementParameters.CanRun;
    private bool CanJump => Grounded && _movementParameters.CanJump || Sloped && _movementParameters.CanJump;
    private bool CanCrouch => Grounded && _movementParameters.CanCrouching || Sloped && _movementParameters.CanCrouching;

    public void RotateAt(Quaternion angle) => _playerOrientation.rotation = angle;
    public void NormalizeMoveSpeed() => _moveSpeed = _movementParameters.WalkSpeed;

    public void NormalizePlayerHeight()
    {
        transform.localScale = new Vector3(transform.localScale.x, _startScaleY, transform.localScale.z);
        _crouching = false;
    }

    public void Move()
    {
        InitializeAxis();

        if (Sloped)
            ApplySlopeMove();
        else
            ApplyDefaultMove();
    }

    public void TryRun()
    {
        if (CanRun)
            _moveSpeed = _movementParameters.RunSpeed;
    }

    public void TryJump()
    {
        if (CanJump)
            _playerRigidbody.AddForce(Vector3.up * _movementParameters.JumpForce, ForceMode.Impulse);
    }

    public void TryCrouching()
    {
        if (CanCrouch)
        {
            transform.localScale = new Vector3(transform.localScale.x, _movementParameters.CrouchScaleY, transform.localScale.z);

            if (Crouching == false)
                _playerRigidbody.AddForce(Vector3.down * _movementParameters.CrouchDownForce, ForceMode.Impulse);

            _moveSpeed = _movementParameters.CrouchSpeed;
            _crouching = true;
        }
    }
    
    private void ApplySlopeMove()
    {
        _playerRigidbody.useGravity = false;
        var slopeGroundNormal = _groundChecker.GetSlopeGroundNormal();

        _moveDirection = Vector3.ProjectOnPlane(
            _playerOrientation.TransformDirection(_moveDirection), slopeGroundNormal).normalized;

       Vector3 slopeMoveVelocity = _moveDirection - Vector3.Dot(_moveDirection, slopeGroundNormal) * slopeGroundNormal;
        slopeMoveVelocity *= _moveSpeed;

        _playerRigidbody.velocity = slopeMoveVelocity;
    }

    private void ApplyDefaultMove()
    {
        _playerRigidbody.useGravity = true;
        Vector3 moveVelocity = _playerOrientation.TransformDirection(_moveDirection) * _moveSpeed;

        if (moveVelocity.magnitude > _moveSpeed)
            moveVelocity = moveVelocity.normalized * _moveSpeed;

        _playerRigidbody.velocity = new Vector3(moveVelocity.x, _playerRigidbody.velocity.y, moveVelocity.z);
    }

    private void InitializeAxis()
    {
        _horizontalAxis = Input.GetAxis(HorizontalAxis);
        _verticalAxis = Input.GetAxis(VerticalAxis);
        _moveDirection = new Vector3(_horizontalAxis, 0f, _verticalAxis);
    }

    private void Start()
    {
        _playerRigidbody = GetComponent<Rigidbody>();
        _playerRigidbody.freezeRotation = true;
        _startScaleY = transform.localScale.y;
    }
}
