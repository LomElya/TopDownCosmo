using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class ButtonManager<T> : MonoBehaviour
{
    public event Action<T> OnClickButton;

    [SerializeField] protected List<MenuButtonSetting> _settings;

    public void Init()
    {
        foreach (var setting in _settings)
        {
            setting.Init();
            setting.Click += OnClick;
        }
    }

    protected virtual void OnClick(T obj, ButtonMain button) => OnClickButton?.Invoke(obj);

    [Serializable]
    protected class MenuButtonSetting
    {
        public event Action<T, ButtonMain> Click;
        [field: SerializeField] public ButtonMain Button { get; private set; }
        [field: SerializeField] public T Obj;

        public void Init() => Button.Click += OnClick;
        private void OnClick() => Click.Invoke(Obj, Button);
        private void OnDestroy() => Button.Click -= OnClick;
    }
}
