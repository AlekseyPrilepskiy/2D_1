using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private Attacker _attacker;
    [SerializeField] private int _damage = 20;
    [SerializeField] private float _range = 1f;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;

    private InputReader _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _playerInput.Attacked += OnAttack;
    }
    private void OnDisable()
    {
        _playerInput.Attacked -= OnAttack;
    }

    private void OnAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(_attackPoint.position, _range, _enemyLayer);

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }
}