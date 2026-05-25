using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : HealthIndicator
{
    [SerializeField] private Slider _slider;

    protected override void UpdateIndicator(float current, float max)
    {
        _slider.maxValue = max;
        _slider.value = current;
    }
}
