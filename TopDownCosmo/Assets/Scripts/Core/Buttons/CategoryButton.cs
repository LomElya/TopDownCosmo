using UnityEngine;
using UnityEngine.UI;

public class CategoryButton : ButtonMain
{
    [SerializeField] private Image _image;
    [SerializeField] private Color _selectColor;
    [SerializeField] private Color _unSelectColor;

    public override void Select() => _image.color = _selectColor;
    public override void UnSelect() => _image.color = _unSelectColor;
}
