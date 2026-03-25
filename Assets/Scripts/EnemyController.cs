using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [SerializeField] private float _visionRadius = 3f;
    [SerializeField] private float _attackRadius = 1.2f;
    [SerializeField] private float _attackCooldown = 1.5f;
    [SerializeField] private int _damage = 10;
    [SerializeField] private LayerMask _playerLayer;

    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;

    private TargetFinder _finder;
    private Patroller _patroller;
    private Chaser _chaser;
    private Attacker _attacker;
    private Mover _mover;
    private Transform _patrolTarget;

    private void Awake()
    {
        _finder = GetComponent<TargetFinder>();
        _patroller = GetComponent<Patroller>();
        _chaser = GetComponent<Chaser>();
        _attacker = GetComponent<Attacker>();
        _mover = GetComponent<Mover>();
        _patrolTarget = _pointB;

        _pointA.SetParent(null);
        _pointB.SetParent(null);

        _patrolTarget = _pointB;
    }

    private void FixedUpdate()
    {
        Transform player = _finder.Find(transform.position, _visionRadius, _playerLayer);

        if (player != null)
        {
            float distance = (player.position - transform.position).sqrMagnitude;

            if (distance <= _attackRadius * _attackRadius)
            {
                _mover.Stop();

                if (player.TryGetComponent(out Health health))
                {
                    _attacker.Action(health, _damage, _attackCooldown);
                }
            }
            else
            {
                _chaser.Execute(player);
            }
        }
        else
        {
            _patroller.Execute(_patrolTarget);

            if (_patroller.Reached(_patrolTarget))
                _patrolTarget = (_patrolTarget == _pointA) ? _pointB : _pointA;
        }
    }
}