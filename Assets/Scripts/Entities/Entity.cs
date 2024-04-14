using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private Health _health;

    public void TakeHeal(float amount)
    {
        _health.ApplyHeal(amount);
    }

    public void TakeDamage(float amount)
    {
        _health.ApplyDamage(amount);

        if (Mathf.Approximately(_health.Value, 0f))
            TryDie();
    }

    protected abstract void TryDie();
}

public interface IDamageable
{
    public void TakeDamage(float amount);
}