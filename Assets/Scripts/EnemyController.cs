using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _stopDistance = 0.04f;

    [SerializeField] private float _visionRadius = 3f;
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private float _attackRadius = 1f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 1f;

    private Rigidbody2D _rigidbody;
    private Transform _patrolTarget;
    private Transform _playerTransform;
    private Health _playerHealth;
    private float _lastAttackTime;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _patrolTarget = _pointB;

        _pointA.SetParent(null);
        _pointB.SetParent(null);
    }

    private void FixedUpdate()
    {
        Find();

        if (_playerTransform != null)
        {
            float distanceToPlayer = ((Vector2)_playerTransform.position - _rigidbody.position).sqrMagnitude;

            if (distanceToPlayer <= _attackRadius * _attackRadius)
            {
                Attack();
            }
            else
            {
                MoveTowards(_playerTransform.position, _chaseSpeed);
            }
        }
        else
        {
            Patrol();
        }
    }

    private void Find()
    {
        Collider2D playerCollider = Physics2D.OverlapCircle(_rigidbody.position, _visionRadius, _playerLayer);

        if (playerCollider != null)
        {
            _playerTransform = playerCollider.transform;

            if (_playerHealth == null)
                _playerTransform.TryGetComponent(out _playerHealth);
        }
        else
        {
            _playerTransform = null;
            _playerHealth = null;
        }
    }

    private void Patrol()
    {
        MoveTowards(_patrolTarget.position, _patrolSpeed);

        float sqrDistance = ((Vector2)_patrolTarget.position - _rigidbody.position).sqrMagnitude;
        if (sqrDistance <= _stopDistance)
        {
            _patrolTarget = (_patrolTarget == _pointA) ? _pointB : _pointA;
        }
    }

    private void MoveTowards(Vector2 targetPos, float speed)
    {
        Vector2 direction = (targetPos - _rigidbody.position).normalized;
        _rigidbody.linearVelocity = new Vector2(direction.x * speed, _rigidbody.linearVelocity.y);
    }

    private void Attack()
    {
        _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);

        if (Time.time >= _lastAttackTime + _attackCooldown && _playerHealth != null)
        {
            _playerHealth.TakeDamage(_damage);
            _lastAttackTime = Time.time;
        }
    }
}