using UnityEngine;

public class InputReader : MonoBehaviour
{
    public float Horizontal { get; private set; }
    public bool IsJumpPressed { get; private set; }

    private void Update()
    {
        Horizontal = Input.GetAxis("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            IsJumpPressed = true;
        }
    }

    public void ResetJump()
    {
        IsJumpPressed = false;
    }
}
