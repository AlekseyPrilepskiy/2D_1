using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private float _stopDistance = 0.5f;

    private Rigidbody2D _rigidbody;
    private Transform _target;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _target = _pointB;

        _pointA.SetParent(null);
        _pointB.SetParent(null);
    }

    private void FixedUpdate()
    {
        Vector2 direction = ((Vector2)_target.position - _rigidbody.position).normalized;
        _rigidbody.linearVelocity = new Vector2(direction.x * _speed, _rigidbody.linearVelocity.y);

        float sqrDistance = ((Vector2)_target.position - _rigidbody.position).sqrMagnitude;

        if (sqrDistance <= _stopDistance * _stopDistance)
        {
            _target = (_target == _pointA) ? _pointB : _pointA;
        }
    }
}
