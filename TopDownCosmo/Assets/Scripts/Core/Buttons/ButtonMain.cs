using System;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonMain : MonoBehaviour
{
    public event Action Click;

    [SerializeField] protected Button _button;

    protected void OnEnable() => _button.onClick.AddListener(OnClick);

    protected void OnDisable() => _button.onClick.RemoveListener(OnClick);

    protected virtual void OnClick() => Click?.Invoke();

    public virtual void Select() {}
    public virtual void UnSelect() {}
}