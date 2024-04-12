using UnityEngine;
using UnityEngine.Events;
using static UnityEngine.GraphicsBuffer;

public class EnemyStateMachine : MonoBehaviour
{
    [SerializeField] private DetectionZone _detectionZone;
    [SerializeField] private AgroZone _agroZone;
    [SerializeField] private Rigidbody2D _rigidbody;
    [SerializeField] private Animator _animator;
    [SerializeField] private Transform _target;
    [SerializeField] private State[] _states;
    [SerializeField] private PlayerDetectedState _playerDetectedState;
    [SerializeField] private State _startState;

    private Transform _transform;
    private State _currentState;
    private bool playerDetected;

    private void Awake()
    {
        _transform = transform;
        _currentState = _startState;
        SetUpStates();
        _startState.Enter(_target);
    }

    private void SetUpStates()
    {
        foreach (var state in _states)     
            state.SetUp(_animator, _transform, _rigidbody);
    }

    private void OnEnable()
    {
        _detectionZone.PlayerDetected += OnPlayerDetected;
        _agroZone.PlayerLost += OnPlayerLost;
    }

    private void OnDisable()
    {
        _detectionZone.PlayerDetected -= OnPlayerDetected;
        _agroZone.PlayerLost -= OnPlayerLost;
    }

    private void Update()
    {
        _currentState.UpdateState(Time.deltaTime);
    }

    private void OnPlayerDetected(Transform player)
    {
        ChangeState(_playerDetectedState, player);
        playerDetected = true;
    }

    private void OnPlayerLost()
    {
        if (playerDetected == false)
            return;

        ChangeState(_startState, _target);
        playerDetected = false;
    }

    private void ChangeState(State next, Transform target)
    {
        _currentState.Exit();
        _currentState = next;
        _currentState.Enter(target);
    }
}