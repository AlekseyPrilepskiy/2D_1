using System;
using System.Collections;
using UnityEngine;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private Health _health;
    [SerializeField] private GameObject _visualArea;

    [SerializeField] private KeyCode _activationKey = KeyCode.E;
    [SerializeField] private int damagePerSecond = 15;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;

    [SerializeField] private LayerMask _enemyLayer;

    private bool _isReady = true;
    public event Action<float, float, bool> StateChanged;

    private void Start()
    {
        if (_visualArea != null)
        {
            _visualArea.SetActive(false);
        }

        StateChanged?.Invoke(_duration, _duration, false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(_activationKey) && _isReady)
        {
            StartCoroutine(ExecuteAbilityRoutine());
        }
    }

    private IEnumerator ExecuteAbilityRoutine()
    {
        _isReady = false;

        if (_visualArea != null)
        {
            _visualArea.SetActive(true);
        }

        float activeTimer = _duration;

        while (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;

            StateChanged?.Invoke(activeTimer, _duration, false);

            Health closestEnemy = FindEnemy();

            if (closestEnemy != null)
            {
                float calculatedValue = damagePerSecond * Time.deltaTime;
                closestEnemy.TakeDamage(calculatedValue);
                _health.Heal(calculatedValue);
            }

            yield return null;
        }

        if (_visualArea != null)
        {
            _visualArea.SetActive(false);
        }

        float cooldownTimer = 0f;

        while (cooldownTimer < _cooldown)
        {
            cooldownTimer += Time.deltaTime;

            StateChanged?.Invoke(cooldownTimer, _cooldown, true);
            yield return null;
        }

        _isReady = true;
        StateChanged?.Invoke(_duration, _duration, false);
    }

    private Health FindEnemy()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, _radius, _enemyLayer);

        Health Enemy = null;
        float minDistance = Mathf.Infinity;

        foreach (var collider in colliders)
        {
            Health enemyHealth = collider.GetComponent<Health>();

            if (enemyHealth != null && enemyHealth.Current > 0)
            {
                float distance = Vector2.Distance(transform.position, collider.transform.position);

                if (distance < minDistance)
                {
                    minDistance = distance;
                    Enemy = enemyHealth;
                }
            }
        }

        return Enemy;
    }
}