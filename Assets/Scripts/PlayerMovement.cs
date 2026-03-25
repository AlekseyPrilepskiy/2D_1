using UnityEngine;

[RequireComponent(typeof(Mover), typeof(InputReader))]
public class PlayerMovement : MonoBehaviour
{
    private Mover _mover;
    private InputReader _playerInput;

    private void Awake()
    {
        _mover = GetComponent<Mover>();
        _playerInput = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _playerInput.Moved += OnMove;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnMove;
    }

    private void OnMove(float direction)
    {
        _mover.SetDirection(direction);
    }
}
