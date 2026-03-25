using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Mover : MonoBehaviour
{
    [SerializeField] private float _speed = 5f;
    private Rigidbody2D _rigidbody;
    private float _direction;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        _rigidbody.linearVelocity = new Vector2(_direction * _speed, _rigidbody.linearVelocity.y);

        if (Mathf.Abs(_direction) > 0.01f)
        {
            float angle = _direction > 0 ? 0 : 180;
            transform.rotation = Quaternion.Euler(0, angle, 0);
        }
    }

    public void SetDirection(float direction)
    {
        _direction = direction;
    }

    public void Stop()
    {
        _direction = 0;
    }
}