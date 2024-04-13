using TMPro;
using UnityEngine;

[RequireComponent(typeof(TMP_Text))]
public class TextHealthbar : Healthbar
{
    private TMP_Text _tmp;

    protected override void OnHealthChanged()
    {
        _tmp.text = $"{Health.Value:f0}/{Health.MaxValue:f0}";
    }

    private void Awake()
    {
        _tmp = GetComponent<TMP_Text>();
    }
}