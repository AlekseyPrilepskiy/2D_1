using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _force = 5f;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private float _radius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private InputReader _playerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<InputReader>();
    }

    private void OnEnable() => _playerInput.Jumped += OnJump;
    private void OnDisable() => _playerInput.Jumped -= OnJump;

    private void OnJump()
    {
        if (IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _force, ForceMode2D.Impulse);
        }
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(_groundPoint.position, _radius, _groundLayer);
}