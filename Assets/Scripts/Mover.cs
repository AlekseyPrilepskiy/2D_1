using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody;
    private InputReader _playerInput;
    private float _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<InputReader>();
    }

    private void OnEnable() => _playerInput.Moved += OnMove;
    private void OnDisable() => _playerInput.Moved -= OnMove;

    private void OnMove(float direction)
    {
        _direction = direction;

        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_direction * _speed, _rigidbody.linearVelocity.y);
    }
}