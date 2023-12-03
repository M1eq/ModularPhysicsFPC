using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private GroundChecker _groundChecker;
    [SerializeField] private PlayerMovementParameters _movementParameters;

    private float _verticalAxis;
    private float _horizontalAxis;
    private Vector3 _moveDirection;
    private Rigidbody _playerRigidbody;
    private const string VerticalAxis = "Vertical";
    private const string HorizontalAxis = "Horizontal";

    private bool Grounded => _groundChecker.GetGroundCheckResult() == true;
    public void RotateAt(Quaternion angle) => transform.rotation = angle;

    public void Move()
    {
        InitializeAxis();

        _moveDirection = new Vector3(_horizontalAxis, 0f, _verticalAxis);
        Vector3 moveVelocity = transform.TransformDirection(_moveDirection) * _movementParameters.MoveSpeed;
        _playerRigidbody.velocity = new Vector3(moveVelocity.x, _playerRigidbody.velocity.y, moveVelocity.z);
    }

    public void TryJump()
    {
        if (Grounded)
            _playerRigidbody.AddForce(Vector3.up * _movementParameters.JumpForce, ForceMode.Impulse);
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
    }
}
