using System.Collections;
using UnityEngine;

public class PlayerDetectedState : State
{
    [SerializeField][Range(0f, 1f)] private float _maxHeightDifference;
    [SerializeField] private GroundCheck _groundCheck;
    [SerializeField] private float _attackDelay;
    [SerializeField] private float _damage;

    private Coroutine _currentCoroutine;
    private IDamageable _player;
    private float _lastSide;

    public override void Enter(Transform followingTarget)
    {
        if (followingTarget.TryGetComponent(out Player player) == false)
            throw new System.InvalidOperationException($"{nameof(followingTarget)} in {nameof(PlayerDetectedState)} " +
                $"expected to be a {nameof(Player)}");

        base.Enter(followingTarget);
        LookToTarget();
        _lastSide = Mathf.Sign(Target.position.x - Transform.position.x);
        _player = player;
    }

    public override void UpdateState(float deltaTime)
    {
        if (TargetReached())
        {
            if (_currentCoroutine == null)
                _currentCoroutine = StartCoroutine(AttackCycle());

            return;
        }
        else if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }

        DoStep();
        TryFlip();
    }

    public override void Exit()
    {
        if (_currentCoroutine != null)
        {
            StopCoroutine(_currentCoroutine);
            _currentCoroutine = null;
        }
    }

    private void DoStep()
    {
        Animator.SetBool(EnemyAnimatorController.Params.IsMoving, true);

        Vector2 direction = (Target.position - transform.position).normalized;
        Vector2 movingVector = Vector2.zero;

        if (_groundCheck.CheckGround())
            movingVector = Vector2.right * Mathf.Sign(direction.x) * MaxSpeed;
        else
            Rigidbody.velocity = Vector2.zero;

        Rigidbody.velocity += movingVector * Time.deltaTime;
        Rigidbody.velocity = new Vector2(Mathf.Clamp(Rigidbody.velocity.x, -MaxSpeed, MaxSpeed), Rigidbody.velocity.y);
    }

    private void TryFlip()
    {
        float side = GetSide();

        if (Mathf.Approximately(side, _lastSide) == false)
            LookToTarget();

        _lastSide = side;
    }

    private IEnumerator AttackCycle()
    {
        Animator.SetBool(EnemyAnimatorController.Params.IsMoving, false);
        Rigidbody.velocity = Vector2.zero;
        var wait = new WaitForSeconds(_attackDelay);

        while (true)
        {
            TryAttack();

            yield return wait;
        }
    }

    private bool TryAttack()
    {
        if (CheckHeight() == false)
            return false;

        _player.ApplyDamage(_damage);

        return true;
    }

    private float GetSide()
    {
        return Mathf.Sign(Target.position.x - Transform.position.x);
    }

    private bool CheckHeight()
    {
        return Mathf.Abs(Target.position.y - Transform.position.y) <= _maxHeightDifference;
    }
}