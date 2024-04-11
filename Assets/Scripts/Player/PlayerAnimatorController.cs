using UnityEngine;

[RequireComponent(typeof(PlayerMovement),typeof(SpriteRenderer))]
public class PlayerAnimatorController : MonoBehaviour
{
    private const string MoveX = nameof(MoveX);
    private const string OnGround = nameof(OnGround);

    private Animator _animator;
    private PlayerMovement _playerMovement;
    private SpriteRenderer _renderer;
    private bool _lastFlipX = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _renderer = GetComponent<SpriteRenderer>();
        _playerMovement = GetComponent<PlayerMovement>();
    }

    private void OnEnable()
    {
        _playerMovement.IsGroundedChanged += OnIsGroundedChanged;
    }

    private void OnDisable()
    {
        _playerMovement.IsGroundedChanged -= OnIsGroundedChanged;
    }

    private void Update()
    {
        _animator.SetFloat(MoveX, Mathf.Abs(_playerMovement.HorizontalSpeed));

        if (Mathf.Approximately(_playerMovement.HorizontalSpeed, 0) == false)
            _lastFlipX = _playerMovement.HorizontalSpeed < 0;

        _renderer.flipX = _lastFlipX;
    }

    private void OnIsGroundedChanged(bool isGrounded)
    {
        _animator.SetBool(OnGround, isGrounded);
    }
}