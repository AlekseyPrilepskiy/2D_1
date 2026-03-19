using UnityEngine;

[RequireComponent(typeof(Rigidbody2D), typeof(InputReader))]
public class Jumper : MonoBehaviour
{
    [SerializeField] private float _jumpForce = 5f;
    [SerializeField] private Transform _groundPoint;
    [SerializeField] private float _groundPointRadius = 0.2f;
    [SerializeField] private LayerMask _groundLayer;

    private Rigidbody2D _rigidbody;
    private InputReader _playerInput;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _playerInput = GetComponent<InputReader>();
    }

    private void FixedUpdate()
    {
        if (_playerInput.IsJumpPressed && IsGrounded())
        {
            _rigidbody.AddForce(Vector2.up * _jumpForce, ForceMode2D.Impulse);
            _playerInput.ResetJump();
        }
    }

    private bool IsGrounded() => Physics2D.OverlapCircle(_groundPoint.position, _groundPointRadius, _groundLayer);
}