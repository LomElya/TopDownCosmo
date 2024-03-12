using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BuyButton : ButtonMain
{
    [SerializeField] private Text _text;

    [Space(10)]
    [SerializeField] private Color _lockColor;
    [SerializeField] private Color _unlockColor;


    [SerializeField, Range(0, 1)] private float _lockAnimationDuration = 0.5f;
    [SerializeField, Range(0.5f, 5)] private float _lockAnimationStrenght = 2f;

    private bool _isLock;

    public void UpdateText(int price)
    {
        gameObject.SetActive(true);
        _text.text = price.ToString();
    }

    public void Hide() => gameObject.SetActive(false);

    public void Lock()
    {
        _isLock = true;
        _text.color = _lockColor;
    }

    public void Unlock()
    {
        _isLock = false;
        _text.color = _unlockColor;
    }



    protected override void OnClick()
    {
        if (_isLock)
        {
            transform.DOShakePosition(_lockAnimationDuration, _lockAnimationStrenght);
            return;
        }

        base.OnClick();
    }
}
