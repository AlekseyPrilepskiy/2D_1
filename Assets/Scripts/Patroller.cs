using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Patroller : MonoBehaviour
{
    [SerializeField] private float _threshold = 0.3f;
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Execute(Transform point)
    {
        float direction = Mathf.Sign(point.position.x - transform.position.x);
        _mover.SetDirection(direction);
    }

    public bool Reached(Transform point)
    {
        float distanceX = Mathf.Abs(point.position.x - transform.position.x);
        return distanceX <= _threshold;
    }
}