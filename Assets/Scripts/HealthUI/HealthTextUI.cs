using UnityEngine;
using TMPro;

public class HealthTextUI : HealthIndicator
{
    [SerializeField] private TMP_Text _healthText;

    protected override void UpdateIndicator(float current, float max)
    {
        _healthText.text = $"{Mathf.RoundToInt(current)}/{Mathf.RoundToInt(max)}";
    }
}
