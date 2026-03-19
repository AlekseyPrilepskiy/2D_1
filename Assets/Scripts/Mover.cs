using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;

    private Rigidbody2D _rigidbody;
    private InputReader _playerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<InputReader>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_playerInput.Horizontal * _speed, _rigidbody.linearVelocity.y);
    }
}