using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _radius = 0.3f;

    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    public bool CheckGround()
    {
        return Physics2D.OverlapCircle(_transform.position, _radius, _groundMask);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
