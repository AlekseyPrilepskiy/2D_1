using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputReader))]
public class PlayerAnimator : MonoBehaviour
{
    private static readonly int s_SpeedHash = Animator.StringToHash("Speed");
    private static readonly int s_AttackHash = Animator.StringToHash("Attack");

    private Animator _animator;
    private InputReader _playerInput;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<InputReader>();
    }

    private void OnEnable()
    {
        _playerInput.Moved += OnHorizontalChanged;
        _playerInput.Attacked += OnAttack;
    }

    private void OnDisable()
    {
        _playerInput.Moved -= OnHorizontalChanged;
        _playerInput.Attacked -= OnAttack;
    }

    private void OnHorizontalChanged(float direction)
    {
        _animator.SetFloat(s_SpeedHash, Mathf.Abs(direction));

        if (direction > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction < 0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void OnAttack()
    {
        _animator.SetTrigger(s_AttackHash);
    }
}
