using UnityEngine;

[RequireComponent(typeof(InputReader))]
public class PlayerAttacker : MonoBehaviour
{
    [SerializeField] private float _attackRange = 1f;
    [SerializeField] private int _damage = 25;
    [SerializeField] private LayerMask _enemyLayer;
    [SerializeField] private Transform _attackPoint;

    private InputReader _playerInput;

    private void Awake()
    {
        _playerInput = GetComponent<InputReader>();
    }

    private void Update()
    {
        if (_playerInput.IsAttackPressed)
        {
            Attack();
            _playerInput.ResetAttack();
        }
    }

    private void Attack()
    {
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(_attackPoint.position, _attackRange, _enemyLayer);

        foreach (Collider2D enemyCollider in hitEnemies)
        {
            if (enemyCollider.TryGetComponent(out Health enemyHealth))
            {
                enemyHealth.TakeDamage(_damage);
            }
        }
    }
}