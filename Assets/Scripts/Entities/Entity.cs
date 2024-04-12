using UnityEngine;

public abstract class Entity : MonoBehaviour, IDamageable
{
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _health;

    private void Awake()
    {
        _health = _maxHealth;
    }

    public void ApplyHeal(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _health += amount;
        _health = Mathf.Clamp(_health, 0f, _maxHealth);
    }

    public void ApplyDamage(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        _health -= amount;
        _health = Mathf.Clamp(_health, 0f, _maxHealth);

        if (Mathf.Approximately(_health, 0f))
            TryDie();
    }

    protected abstract void TryDie();
}

public interface IDamageable
{
    public void ApplyDamage(float amount);
}