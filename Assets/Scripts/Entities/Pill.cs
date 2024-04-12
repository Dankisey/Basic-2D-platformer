using UnityEngine;

public class Pill : MonoBehaviour
{
    [SerializeField] private float _healthRestore;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Entity entity))
        {
            entity.ApplyHeal(_healthRestore);
            Destroy(gameObject);
        }
    }
}