using UnityEngine;

public class Patroller : MonoBehaviour
{
    [SerializeField] private float _stopThreshold = 0.2f;

    public void Move(Rigidbody2D rigidbody, Vector2 targetPosition, float speed)
    {
        Vector2 direction = (targetPosition - rigidbody.position).normalized;
        rigidbody.linearVelocity = new Vector2(direction.x * speed, rigidbody.linearVelocity.y);
    }

    public bool Reached(Vector2 current, Vector2 target)
    {
        return (target - current).sqrMagnitude <= _stopThreshold * _stopThreshold;
    }
}