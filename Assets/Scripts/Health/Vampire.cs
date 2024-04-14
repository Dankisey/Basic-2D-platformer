using System.Collections;
using UnityEngine;

public class Vampire : MonoBehaviour
{
    [SerializeField] private Health _health; 
    [SerializeField][Range(0, 0.1f)] private float _steelAmount;
    [SerializeField] private LayerMask _enemyMask;
    [SerializeField] private float _duration;
    [SerializeField] private float _radius;

    private Coroutine _currentCoroutine;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_currentCoroutine != null)
                StopCoroutine(_currentCoroutine);

            _currentCoroutine = StartCoroutine(SteelHealth());
        }
    }

    private IEnumerator SteelHealth()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _duration) 
        {
            Collider2D collider = Physics2D.OverlapCircle(_transform.position, _radius, _enemyMask);

            if (collider != null)
            {
                collider.TryGetComponent(out Enemy enemy);
                float stolenAmount = enemy.StealHealth(_steelAmount);
                _health.ApplyHeal(stolenAmount);
            }

            yield return null;

            elapsedTime += Time.deltaTime;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}