using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _slopeRayLenght;
    [SerializeField] private float _sphereRadius;

    private RaycastHit _slopeHit;

    public Vector3 GetSlopeGroundNormal() => _slopeHit.normal;
    public bool GetGroundCheckResult() => Physics.CheckSphere(transform.position, _sphereRadius, _groundMask);

    public bool GetSlopeGroundCheckResult()
    {
        if (Physics.Raycast(transform.position, Vector3.down, out _slopeHit, _slopeRayLenght))
        {
            if (_slopeHit.normal != Vector3.up)
                return true;
            else
                return false;
        }

        return false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _sphereRadius);

        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, new Vector3(0, -_slopeRayLenght, 0));
    }
}
