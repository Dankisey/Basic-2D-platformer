using System.Collections;
using UnityEngine;

public class SmoothImageHealthbar : ImageHealthbar
{
    [SerializeField][Range(0, 1)] private float _maxChangingDelta;

    private Coroutine _currentCoroutine = null;

    protected override void ChangeSliderValue(float normalizedValue)
    {
        if (_currentCoroutine != null)   
            StopCoroutine(_currentCoroutine);    

        _currentCoroutine = StartCoroutine(ChangeValueSmooth(normalizedValue));
    }

    private IEnumerator ChangeValueSmooth(float targetValue)
    {
        while (Mathf.Approximately(Fill.fillAmount, targetValue) == false)
        {
            Fill.fillAmount = Mathf.MoveTowards(Fill.fillAmount, targetValue, _maxChangingDelta * Time.deltaTime);

            yield return null;
        }

        _currentCoroutine = null;
    }
}