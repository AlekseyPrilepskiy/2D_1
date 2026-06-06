using UnityEngine;
using UnityEngine.UI;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private VampirismAbility _abilitySource;
    [SerializeField] private Slider _slider;
    [SerializeField] private Image _fill;
    [SerializeField] private Color _activeColor = Color.red;
    [SerializeField] private Color _cooldownColor = Color.gray;

    private void OnEnable()
    {
        if (_abilitySource != null)
        {
            _abilitySource.StateChanged += UpdateSlider;
        }
    }

    private void OnDisable()
    {
        if (_abilitySource != null)
        {
            _abilitySource.StateChanged -= UpdateSlider;
        }
    }

    private void UpdateSlider(float current, float max, bool isCooldown)
    {
        _slider.maxValue = max;
        _slider.value = current;

        if (_fill != null)
        {
            _fill.color = isCooldown ? _cooldownColor : _activeColor;
        }
    }
}
