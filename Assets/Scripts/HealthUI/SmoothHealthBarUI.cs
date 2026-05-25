using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthBarUI : HealthIndicator
{
    [SerializeField] private Slider _slider;
    [SerializeField] private float _smoothSpeed = 30f;

    private Coroutine _updateBarCoroutine;
    private bool _isInitialized = false;

    protected override void OnDisable()
    {
        base.OnDisable();

        StopCoroutine(_updateBarCoroutine);
    }

    protected override void UpdateIndicator(float current, float max)
    {
        _slider.maxValue = max;

        if (!_isInitialized)
        {
            _slider.value = current;
            _isInitialized = true;
            return;
        }

        if (_updateBarCoroutine != null)
        {
            StopCoroutine(_updateBarCoroutine);
        }

        _updateBarCoroutine = StartCoroutine(SmoothChangeValue(current));
    }

    private IEnumerator SmoothChangeValue(float targetValue)
    {
        while (!Mathf.Approximately(_slider.value, targetValue))
        {
            _slider.value = Mathf.MoveTowards(
                _slider.value,
                targetValue,
                _smoothSpeed * Time.deltaTime
            );
            yield return null;
        }

        _slider.value = targetValue;
        _updateBarCoroutine = null;
    }
}
