using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _minHeight;

    private Vector2 _startPosition;

    public Vector2 StartPosition => _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < _minHeight)
        {
            transform.position = _startPosition;
        }
    }
}