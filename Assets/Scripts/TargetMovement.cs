using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]

public class TargetMovement : MonoBehaviour
{
    [SerializeField] private int _timeToStay;
    [SerializeField] private Transform _target;
    [SerializeField] private float _speed;

    private SpriteRenderer _renderer;
    private Animator _animator;
    private Vector2 _startPosition;
    private float _tempSpeed;
    private bool _isWaiting;

    private void Start()
    {
        _startPosition = transform.position;
        _renderer = GetComponent<SpriteRenderer>();
        _animator = GetComponent<Animator>();

        _tempSpeed = _speed;

        _animator.SetBool(EnemyAnimatorController.States.IsMoving, true);
    }

    private void Update()
    {
        float xPosition = Mathf.MoveTowards(transform.position.x, _target.position.x, _speed * Time.deltaTime) - transform.position.x;
        transform.position += new Vector3(xPosition, 0);

        if (transform.position.x <= _startPosition.x && _isWaiting == false || transform.position.x >= _target.position.x && _isWaiting == false)
        {
            _tempSpeed *= -1;

            _isWaiting = true;
            StartCoroutine(Wait());
        }
    }

    private IEnumerator Wait()
    {
        var waitForNeedSeconds = new WaitForSeconds(_timeToStay);

        _animator.SetBool(EnemyAnimatorController.States.IsMoving, false);
        _speed = 0;

        yield return waitForNeedSeconds;

        _animator.SetBool(EnemyAnimatorController.States.IsMoving, true);
        _speed = _tempSpeed;

        Flip();

        _isWaiting = false;
    }

    private void Flip()
    {
        _renderer.flipX = !_renderer.flipX;
    }
}
