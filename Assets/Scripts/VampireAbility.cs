using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class VampirismAbility : MonoBehaviour
{
    [SerializeField] private Health _playerHealth;
    [SerializeField] private GameObject _vampireVisualSphere;
    [SerializeField] private float _radius = 3f;
    [SerializeField] private float _damagePerSecond = 15f;
    [SerializeField] private float _duration = 6f;
    [SerializeField] private float _cooldown = 4f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private int _maxTargetCount = 5;

    public event Action<float, float, bool> StateChanged;

    private Collider2D[] _overlapResults;
    private InputReader _playerInput;
    private bool _isReady = true;

    private void Awake()
    {
        _overlapResults = new Collider2D[_maxTargetCount];
        _playerInput = GetComponent<InputReader>();
    }

    private void Start()
    {
        if (_vampireVisualSphere != null)
        {
            _vampireVisualSphere.SetActive(false);
        }

        StateChanged?.Invoke(_duration, _duration, false);
    }

    private void OnEnable()
    {
        _playerInput.Vampired += ActivateAbility;
    }

    private void OnDisable()
    {
        _playerInput.Vampired -= ActivateAbility;
    }

    private void ActivateAbility()
    {
        if (_isReady)
        {
            StartCoroutine(ActiveAbilityRoutine());
        }
    }

    private IEnumerator ActiveAbilityRoutine()
    {
        _isReady = false;

        if (_vampireVisualSphere != null)
        {
            _vampireVisualSphere.SetActive(true);
        }

        float activeTimer = _duration;

        while (activeTimer > 0)
        {
            activeTimer -= Time.deltaTime;
            StateChanged?.Invoke(activeTimer, _duration, false);

            Health closestEnemy = FindClosestEnemy();

            if (closestEnemy != null)
            {
                float calculatedValue = _damagePerSecond * Time.deltaTime;
                closestEnemy.TakeDamage(calculatedValue);
                _playerHealth.Heal(calculatedValue);
            }

            yield return null;
        }

        if (_vampireVisualSphere != null)
        {
            _vampireVisualSphere.SetActive(false);
        }

        yield return StartCoroutine(CooldownRoutine());
    }

    private IEnumerator CooldownRoutine()
    {
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

    private Health FindClosestEnemy()
    {
        int count = Physics2D.OverlapCircleNonAlloc(transform.position, _radius, _overlapResults, _enemyLayer);

        Health closestEnemy = null;

        float minSqrDistance = Mathf.Infinity;
        Vector2 currentPosition = transform.position;

        for (int i = 0; i < count; i++)
        {
            Collider2D collider = _overlapResults[i];

            if (collider == null)
            {
                continue;
            }

            Health enemyHealth = collider.GetComponent<Health>();

            if (enemyHealth != null && enemyHealth.Current > 0)
            {
                Vector2 direction = (Vector2)collider.transform.position - currentPosition;
                float sqrDistance = direction.sqrMagnitude;

                if (sqrDistance < minSqrDistance)
                {
                    minSqrDistance = sqrDistance;
                    closestEnemy = enemyHealth;
                }
            }
        }

        return closestEnemy;
    }
}