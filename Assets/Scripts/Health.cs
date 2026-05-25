using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max = 100;
    public float Current { get; private set; }

    public event Action<float, float> Changed;

    private void Start()
    {
        Current = _max;
        Changed?.Invoke(Current, _max);
    }

    public void TakeDamage(int damage)
    {
        if (Current > 0)
        {
            Current -= damage;
            Current = Mathf.Clamp(Current, 0, _max);
            Changed?.Invoke(Current, _max);
        }

        if (Current <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (Current > 0)
        {
            Current += amount;

            if (Current >= _max)
            {
                Current = _max;
            }

            Current = Mathf.Clamp(Current, 0, _max);
            Changed?.Invoke(Current, _max);
        }
    }

    private void Die()
    {
        Destroy(gameObject);
        Destroy(transform.parent.gameObject);
    }
}
