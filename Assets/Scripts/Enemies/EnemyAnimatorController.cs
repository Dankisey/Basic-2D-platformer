using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyAnimatorController : MonoBehaviour
{
    private const string IsMoving = nameof(IsMoving);

    [SerializeField] private EnemyMovement _enemyMovement;

    private Animator _animator;
    private SpriteRenderer _renderer;
    private bool _lastFlipX = false;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _animator.SetBool(IsMoving, true);
        _renderer = GetComponent<SpriteRenderer>();
    }

    private void OnEnable()
    {
        _enemyMovement.SpeedChanged += OnSpeedChanged;
    }

    private void OnDisable()
    {
        _enemyMovement.SpeedChanged -= OnSpeedChanged;
    }

    private void OnSpeedChanged(float value)
    {
        _animator.SetBool(IsMoving, Mathf.Abs(value) > 0);

        if (Mathf.Approximately(value, 0) == false)
            _lastFlipX = value > 0;

        _renderer.flipX = _lastFlipX;
    }
}