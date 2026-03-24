using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(TargetFinder), typeof(Patroller), typeof(Attacker))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _visionRadius = 3f;
    [SerializeField] private float _attackRadius = 1.2f;
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private float _patrolSpeed = 2f;
    [SerializeField] private float _chaseSpeed = 3f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private float _attackCooldown = 1.5f;

    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    private Rigidbody2D _rigidbody;
    private TargetFinder _finder;
    private Patroller _patroller;
    private Attacker _attacker;

    private Transform _targetPoint;
    private Transform _player;
    private Health _playerHealth;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _finder = GetComponent<TargetFinder>();
        _patroller = GetComponent<Patroller>();
        _attacker = GetComponent<Attacker>();

        _targetPoint = _pointB;

        _pointA.SetParent(null);
        _pointB.SetParent(null);
    }

    private void FixedUpdate()
    {
        _player = _finder.Find(transform.position, _visionRadius, _playerLayer);

        if (_player != null)
        {
            HandleChaseAndAttack();
        }
        else
        {
            HandlePatrol();
        }
    }

    private void HandleChaseAndAttack()
    {
        float sqrDistance = ((Vector2)_player.position - _rigidbody.position).sqrMagnitude;

        if (sqrDistance <= _attackRadius * _attackRadius)
        {
            _rigidbody.linearVelocity = new Vector2(0, _rigidbody.linearVelocity.y);

            if (_playerHealth == null)
            {
                _player.TryGetComponent(out _playerHealth);
            }

            if (_playerHealth != null)
            {
                _attacker.Action(_playerHealth, _damage, _attackCooldown);
            }
        }
        else
        {
            MoveTo(_player.position, _chaseSpeed);
        }
    }

    private void HandlePatrol()
    {
        MoveTo(_targetPoint.position, _patrolSpeed);

        if (_patroller.Reached(_rigidbody.position, _targetPoint.position))
        {
            _targetPoint = (_targetPoint == _pointA) ? _pointB : _pointA;
        }
    }

    private void MoveTo(Vector3 position, float speed)
    {
        _patroller.Move(_rigidbody, position, speed);

        float direction = position.x - transform.position.x;

        if (Mathf.Abs(direction) > 0.01f)
        {
            if (direction > 0)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0);
            }
        }
    }
}