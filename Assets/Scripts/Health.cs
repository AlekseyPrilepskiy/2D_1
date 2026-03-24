using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _max = 100;
    private int _current;

    private void Start()
    {
        _current = _max;
    }

    public void TakeDamage(int damage)
    {
        if (_current > 0)
        {
            _current -= damage;
        }

        if (_current <= 0)
        {
            Die();
        }
    }

    public void Heal(int amount)
    {
        if (_current > 0)
        {
            _current += amount;

            if (_current >= _max)
            {
                _current = _max;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
