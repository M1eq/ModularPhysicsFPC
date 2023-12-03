using UnityEngine;

public class GroundChecker : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _radius;

    public bool GetGroundCheckResult() => Physics.CheckSphere(transform.position, _radius, _groundMask);

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.blue;
        Gizmos.DrawSphere(transform.position, _radius);
    }
}
