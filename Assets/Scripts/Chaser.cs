using UnityEngine;

[RequireComponent(typeof(Mover))]
public class Chaser : MonoBehaviour
{
    private Mover _mover;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
    }

    public void Execute(Transform target)
    {
        float direction = Mathf.Sign(target.position.x - transform.position.x);
        _mover.SetDirection(direction);
    }
}
