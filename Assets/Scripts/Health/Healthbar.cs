using System.Collections;
using UnityEngine;

public abstract class Healthbar : MonoBehaviour
{
    [SerializeField] protected Health Health;

    protected abstract void OnHealthChanged();

    private void OnEnable()
    {
        Health.Changed += OnHealthChanged;
    }

    private void OnDisable()
    {
        Health.Changed -= OnHealthChanged;
    }
}
