using System.Collections;
using UnityEngine;

public class PatrolState : State
{
    [SerializeField] private Transform[] _patrolBorders;
    [SerializeField] private int _timeToStay;

    private Coroutine _currentCoroutine = null;
    private int _currentTargetIndex;
    private bool _isWaiting;

    public override void Enter(Transform followingTarget)
    {
        _currentTargetIndex = 0;
        _isWaiting = false;
        base.Enter(followingTarget);
    }

    public override void UpdateState(float deltaTime)
    {
        if (_isWaiting == false)
            DoStep();

        if (_currentCoroutine == null && TargetReached())
        {
            _isWaiting = true;
            Rigidbody.velocity = Vector3.zero;
            _currentCoroutine = StartCoroutine(Wait());
        }
    }

    private void DoStep()
    {
        Animator.SetBool(EnemyAnimatorController.Params.IsMoving, true);
        Vector2 movingVector = (Target.position - transform.position).normalized * MaxSpeed;
        movingVector.y = 0;
        Rigidbody.velocity = movingVector;
    }

    private void SwapTarget()
    {
        Target = _patrolBorders[_currentTargetIndex];
        _currentTargetIndex = ++_currentTargetIndex % _patrolBorders.Length;
    }

    private IEnumerator Wait()
    {
        Animator.SetBool(EnemyAnimatorController.Params.IsMoving, false);

        yield return new WaitForSeconds(_timeToStay);

        SwapTarget();
        LookToTarget();
        _isWaiting = false;

        _currentCoroutine = null;
        Animator.SetBool(EnemyAnimatorController.Params.IsMoving, true);
    }
}