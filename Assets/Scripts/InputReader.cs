using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<float> Moved;
    public event Action Jumped;
    public event Action Attacked;

    private float _lastHorizontal;

    private void Update()
    {
        float horizontal = Input.GetAxis("Horizontal");
        if (Mathf.Abs(horizontal - _lastHorizontal) > 0.01f)
        {
            _lastHorizontal = horizontal;
            Moved?.Invoke(horizontal);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jumped?.Invoke();
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            Attacked?.Invoke();
        }
    }
}
