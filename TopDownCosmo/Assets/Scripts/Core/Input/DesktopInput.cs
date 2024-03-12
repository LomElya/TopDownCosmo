using System;
using UnityEngine;

public class DesktopInput : IInput
{
    public event Action<Vector2> ClickButtonMove;

    public float Horizontal => Input.GetAxisRaw("Horizontal");
    public float Vertical => Input.GetAxisRaw("Vertical");
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    public bool IsMooving => Input.GetButton("Horizontal") || Input.GetButton("Vertical");

    public void Tick()
    {
        if (!IsMooving)
            return;

        ClickButtonMove?.Invoke(Direction);
    }
}
