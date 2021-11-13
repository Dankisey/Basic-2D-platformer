using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]

public class Movement : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private float _jumpForce;
    [SerializeField] private float _angle;
    [SerializeField] private float _raycastDistance;
    [SerializeField] private ContactFilter2D _contactFilter;

    private const string Horizontal = "Horizontal";

    private SpriteRenderer _renderer;
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private Vector2 _movementDirection;

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        Move();
        Flip();
        bool onGround = CheckGround();

        if (Input.GetKey(KeyCode.Space))
        {
            TryJump(onGround);
        }
    }

    private void Move()
    {
        _movementDirection.x = Input.GetAxis(nameof(Horizontal));
        _animator.SetFloat(PlayerAnimatorController.Params.MoveX, Mathf.Abs(_movementDirection.x));
        transform.Translate(_speed * Time.deltaTime * _movementDirection.x, 0, 0);
    }

    private void Flip()
    {
        _renderer.flipX = _movementDirection.x < 0;
    }

    private bool CheckGround()
    {
        RaycastHit2D[] results = new RaycastHit2D[1];
        int contacts = Physics2D.BoxCast(transform.position, transform.localScale, _angle, Vector2.down, _contactFilter, results, _raycastDistance);

        if (contacts > 0)
        {
            _animator.SetBool(PlayerAnimatorController.States.OnGround, true);
            return true;
        }
        else
        {
            _animator.SetBool(PlayerAnimatorController.States.OnGround, false);
            return false;
        }
    }

    private void TryJump(bool onGround)
    {
        if (onGround)            
            _rigidbody.velocity = new Vector2(_rigidbody.velocity.x, _jumpForce);      
    }
}