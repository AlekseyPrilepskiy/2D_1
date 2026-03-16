using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform _pointA;
    [SerializeField] private Transform _pointB;
    [SerializeField] private float _speed = 2f;

    private Vector3 _targetWorldPosition;
    private Transform _currentTargetTransform;
    private Rigidbody2D _rigidbody;

    private void Start()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _pointA.SetParent(null);
        _pointB.SetParent(null);

        _currentTargetTransform = _pointB;
        UpdateTargetPosition();
    }

    private void FixedUpdate()
    {
        UpdateTargetPosition();

        Vector2 direction = ((Vector2)_targetWorldPosition - _rigidbody.position).normalized;
        _rigidbody.linearVelocity = new Vector2(direction.x * _speed, _rigidbody.linearVelocity.y);

        if (Vector2.Distance(_rigidbody.position, (Vector2)_targetWorldPosition) <= 0.5f)
        {
            _currentTargetTransform = (_currentTargetTransform == _pointA) ? _pointB : _pointA;
        }
    }

    private void UpdateTargetPosition()
    {
        _targetWorldPosition = _currentTargetTransform.position;
    }
}
