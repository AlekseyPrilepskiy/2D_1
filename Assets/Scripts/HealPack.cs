using UnityEngine;

public class HealthPack : MonoBehaviour
{
    [SerializeField] private int _healAmount = 25;

    public void Collect(Health targetHealth)
    {
        targetHealth.Heal(_healAmount);
        Destroy(gameObject);
    }
}
