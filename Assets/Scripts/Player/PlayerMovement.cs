using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private const string Horizontal = nameof(Horizontal);

    [Header("Gameplay Settings")]
    [SerializeField] private float _speed = 3;
    [SerializeField] private float _jumpForce = 11;

    [Space]
    [Header("Physiccheck settings")]
    [SerializeField] private LayerMask _groundMask;
    [SerializeField] private Transform _groundCheck;
    [SerializeField] private float _circleRadius = 0.3f;
    [SerializeField] private float _wallCheckOffset = 0.06f;

    private ContactFilter2D _contactFilter;
    private Rigidbody2D _rigidbody;
    private bool _isGrounded = true;

    public float HorizontalSpeed { get; private set; }

    public event UnityAction<bool> IsGroundedChanged;

    private void Awake()
    {
        _contactFilter.layerMask = _groundMask;
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        CheckGround();
        Move();

        if (Input.GetKey(KeyCode.Space))
            TryJump();
    }

    private void Move()
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        HorizontalSpeed = Input.GetAxis(Horizontal) * _speed;
        int hits = _rigidbody.Cast(Vector2.right * Mathf.Sign(HorizontalSpeed), _contactFilter, results, _wallCheckOffset);

        if (hits > 0)
            HorizontalSpeed = 0f;

        _rigidbody.velocity = new Vector2(HorizontalSpeed, _rigidbody.velocity.y);
    }

    private void CheckGround()
    {
        bool isGrounded = Physics2D.OverlapCircle(_groundCheck.position, _circleRadius, _groundMask);

        if (isGrounded != _isGrounded)
            IsGroundedChanged?.Invoke(isGrounded);

        _isGrounded = isGrounded;
    }

    private void TryJump()
    {
        if (_isGrounded)
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(_groundCheck.position, _circleRadius);

        Gizmos.color = Color.blue;
        Vector3 offset = Mathf.Abs(HorizontalSpeed) > 0 ? new Vector3(_wallCheckOffset * Mathf.Sign(HorizontalSpeed), 0) : Vector3.zero;
        Vector3 center = offset + transform.position;
        Gizmos.DrawWireCube(center, new Vector3(0.8f,1));
    }
}