using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;

    public void ApplyHeal(float amount)
    {
        _health.TakeHeal(amount);
    }

    public void ApplyDamage(float amount)
    {
        _health.TakeDamage(amount);

        if (Mathf.Approximately(_health.Value, 0f))
            TryDie();
    }

    protected abstract void TryDie();
}

public interface IDamageable
{
    public void ApplyDamage(float amount);
}