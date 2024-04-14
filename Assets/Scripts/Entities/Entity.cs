using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;

    public float StealHealth(float amount)
    {
        return _health.Steal(amount);
    }

    public void TakeHeal(float amount)
    {
        _health.ApplyHeal(amount);
    }

    public void TakeDamage(float amount)
    {
        _health.ApplyDamage(amount);  
    }

    protected abstract void TryDie();

    private void OnHealthChanged()
    {
        if (Mathf.Approximately(_health.Value, 0f))
            TryDie();
    }

    private void OnEnable()
    {
        _health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        _health.Changed -= OnHealthChanged;
    }
}

public interface IDamageable
{
    public void TakeDamage(float amount);
}