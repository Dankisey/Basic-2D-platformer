using UnityEngine;

public class Player : MonoBehaviour
{
    private Vector2 _startPosition;

    public Vector2 StartPosition => _startPosition;

    private void Start()
    {
        _startPosition = transform.position;
    }

    private void FixedUpdate()
    {
        if (transform.position.y < -6)
        {
            transform.position = _startPosition;
        }
    }
}