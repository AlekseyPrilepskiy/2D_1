using UnityEngine;

[RequireComponent(typeof(Animator), typeof(InputReader), typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour
{
    private Animator _animator;
    private InputReader _playerInput;
    private SpriteRenderer _spriteRenderer;

    private static readonly int s_SpeedHash = Animator.StringToHash("Speed");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _playerInput = GetComponent<InputReader>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        float horizontalMove = _playerInput.Horizontal;
        _animator.SetFloat(s_SpeedHash, Mathf.Abs(horizontalMove));

        if (horizontalMove > 0)
        {
            _spriteRenderer.flipX = false;
        }
        else if (horizontalMove < 0)
        {
            _spriteRenderer.flipX = true;
        }
    }
}
