using UnityEngine;
using UnityEngine.UI;

public class ImageHealthbar : Healthbar
{
    [SerializeField] protected Image Fill;

    protected override void OnHealthChanged()
    {
        float normalizedValue = Health.Value / Health.MaxValue;
        ChangeSliderValue(normalizedValue);
    }

    protected virtual void ChangeSliderValue(float normalizedValue)
    {
        Fill.fillAmount = normalizedValue;
    }
}