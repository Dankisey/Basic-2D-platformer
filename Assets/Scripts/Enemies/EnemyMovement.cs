using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private Transform[] _patrolBorders;
    [SerializeField] private int _timeToStay;
    [SerializeField] private float _speed;
    [SerializeField] private float _newTargetDistance = 0.01f;

    private Transform _target;
    private Transform _transform;
    private bool _isWaiting;
    private int _currentTargetIndex = 0;

    public event UnityAction<float> SpeedChanged;

    private void Awake()
    {
        SwapTarget();
        _transform = transform;
    }

    private void Update()
    {
        float nextPositionX = Mathf.MoveTowards(_transform.position.x, _target.position.x, Mathf.Abs(_speed) * Time.deltaTime);
        float movingDelta = nextPositionX - _transform.position.x;
        _transform.position += new Vector3(movingDelta, 0);

        if (_isWaiting == false && Mathf.Abs(_target.position.x - _transform.position.x) <= _newTargetDistance)
        {
            _isWaiting = true;
            SwapTarget();
            StartCoroutine(Wait());
        }
    }

    private void SetSpeed(float value)
    {
        _speed = value;
        SpeedChanged?.Invoke(value);
    }

    private void SwapTarget()
    {
        _target = _patrolBorders[_currentTargetIndex];
        _currentTargetIndex = ++_currentTargetIndex % _patrolBorders.Length;
    }

    private IEnumerator Wait()
    {
        float lastSpeed = _speed;
        SetSpeed(0);

        yield return new WaitForSeconds(_timeToStay);

        SetSpeed(-lastSpeed);
        _isWaiting = false;
    }
}