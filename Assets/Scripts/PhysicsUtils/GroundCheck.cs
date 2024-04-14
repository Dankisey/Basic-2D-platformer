using UnityEngine;
using UnityEngine.Events;

public class GroundCheck : MonoBehaviour
{
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private float _radius = 0.3f;
    [SerializeField] private float _distance = 0.1f;

    private Transform _transform;

    public event UnityAction<RaycastHit2D> Hitted;

    private void Awake()
    {
        _transform = transform;
    }

    public bool CheckGround()
    {
        RaycastHit2D hit = Physics2D.CircleCast(_transform.position, _radius, Vector2.down, _distance, _groundMask);
        bool hitted = hit.collider != null;

        if (hitted)
            Hitted?.Invoke(hit);
        
        return hitted;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
