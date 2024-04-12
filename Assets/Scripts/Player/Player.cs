using UnityEngine;

public class Player : Entity
{
    [SerializeField] private float _minHeight;

    private Vector2 _startPosition;

    protected override void TryDie()
    {
        transform.position = _startPosition;
    }

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        if (transform.position.y < _minHeight)      
            TryDie();
    }
}