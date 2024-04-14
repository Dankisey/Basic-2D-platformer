using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    [SerializeField] private float _maxValue;

    public float Value { get; private set; }
    public float MaxValue => _maxValue;

    public event UnityAction Changed;

    private void Start()
    {
        Value = _maxValue;
        Changed?.Invoke();
    }

    public float Steal(float amount)
    { 
        amount = Mathf.Clamp(amount, 0, Value);
        ApplyDamage(amount);

        return amount;
    }
 
    public void ApplyDamage(float amount)
    {
        if (amount < 0) 
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        Value = Mathf.Clamp(Value - amount, 0, _maxValue);
        Changed?.Invoke();
    }

    public void ApplyHeal(float amount)
    {
        if (amount < 0)
            throw new System.ArgumentOutOfRangeException(nameof(amount));

        Value = Mathf.Clamp(Value + amount, 0, _maxValue);
        Changed?.Invoke();
    }   
}