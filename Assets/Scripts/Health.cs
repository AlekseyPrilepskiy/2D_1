using System;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private int _maxHealth = 100;

    private int _currentHealth;

    private void Start()
    {
        _currentHealth = _maxHealth;
    }

    public void TakeDamage(int damage)
    {
        if (_currentHealth > 0)
        {
            _currentHealth -= damage;

            if (_currentHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Heal(int amount)
    {
        if (_currentHealth > 0)
        {
            _currentHealth += amount;

            if (_currentHealth >= _maxHealth)
            {
                _currentHealth = _maxHealth;
            }
        }
    }

    private void Die()
    {
        Destroy(gameObject);
    }
}
