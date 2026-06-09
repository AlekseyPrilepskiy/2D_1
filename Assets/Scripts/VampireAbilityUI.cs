using UnityEngine;
using UnityEngine.UI;

public class VampirismUI : MonoBehaviour
{
    [SerializeField] private VampirismAbility _ability;
    [SerializeField] private Slider _slider;
    [SerializeField] private Color _activeColor = Color.blue;
    [SerializeField] private Color _cooldownColor = Color.gray;

    private Image _sliderFillImage;

    private void Awake()
    {
        if (_slider != null && _slider.fillRect != null)
        {
            _sliderFillImage = _slider.fillRect.GetComponent<Image>();
        }
    }

    private void OnEnable()
    {
        _ability.StateChanged += OnAbilityStateChanged;
    }

    private void OnDisable()
    {
        _ability.StateChanged -= OnAbilityStateChanged;
    }

    private void OnAbilityStateChanged(float current, float max, bool isCooldown)
    {
        if (_slider == null)
        {
            return;
        }

        _slider.maxValue = max;
        _slider.value = current;

        if (_sliderFillImage != null)
        {
            _sliderFillImage.color = isCooldown ? _cooldownColor : _activeColor;
        }
    }
}
