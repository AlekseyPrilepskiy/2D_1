using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollower : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothSpeed = 0f;
    [SerializeField] private Vector3 _offset = new Vector3(0, 2, -10);

    private Vector3 _currentVelocity = Vector3.zero;

    private void LateUpdate()
    {
        Vector3 targetPosition = _target.position + _offset;

        transform.position = Vector3.SmoothDamp(
            transform.position,
            targetPosition,
            ref _currentVelocity,
            _smoothSpeed
        );
    }
}
