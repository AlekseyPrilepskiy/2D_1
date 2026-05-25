using UnityEngine;

public abstract class HealthIndicator : MonoBehaviour
{
    [SerializeField] protected Health HealthData;

    protected virtual void OnEnable()
    {
        HealthData.Changed += UpdateIndicator;
    }

    protected virtual void OnDisable()
    {
        HealthData.Changed -= UpdateIndicator;
    }

    protected abstract void UpdateIndicator(float current, float max);
}
