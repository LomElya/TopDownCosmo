using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(Image))]
public abstract class SelectableMenuSlot : MenuSlot, IPointerClickHandler
{
    public event Action<SelectableMenuSlot> Click;

    [SerializeField] private Sprite _standartBackground;
    [SerializeField] private Sprite _highlightBackground;

    [SerializeField] private Image _lockImage;
    [SerializeField] private Image _seletcImage;

    public bool IsLock { get; private set; }
    public bool Selected { get; private set; }

    protected Image _backgroundImage;

    protected void Init()
    {
        _backgroundImage = GetComponent<Image>();
        _backgroundImage.sprite = _standartBackground;
    }

    public virtual void Lock()
    {
        IsLock = true;

        _lockImage.gameObject.SetActive(IsLock);
    }

    public virtual void UnLock()
    {
        IsLock = false;

        _lockImage.gameObject.SetActive(IsLock);
    }

    public virtual void Select()
    {
        Selected = true;
        _seletcImage.gameObject.SetActive(Selected);
    }

    public virtual void UnSelect()
    {
        Selected = false;
        _seletcImage.gameObject.SetActive(Selected);
    }

    public void OnPointerClick(PointerEventData eventData) => Click?.Invoke(this);
    public virtual void Highlight() => _backgroundImage.sprite = _highlightBackground;
    public virtual void UnHighlight() => _backgroundImage.sprite = _standartBackground;
}
