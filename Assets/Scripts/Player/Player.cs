using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _minHeight;

    private Vector2 _startPosition;

    public void Die()
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
            Die();
    }
}