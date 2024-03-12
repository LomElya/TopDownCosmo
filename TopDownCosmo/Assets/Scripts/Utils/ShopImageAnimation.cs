using DG.Tweening;
using UnityEngine;

public class ShopImageAnimation : MonoBehaviour
{
    [SerializeField] private float _durationAnimation;

    private const float _delay = 0.1f;

    private const float _startPoint = 0f;
    private const float _endPoint = 0.2f;

    private Vector2 _startVector => Vector2.zero;
    private Vector2 _endVector => new Vector2(0, 0.1f);

    private Tween _tween;

    private void Start()
    {
        _tween = DOTween.Sequence()
             .Append(transform.DOLocalMoveY(_endPoint, _durationAnimation))
             .Append(transform.DOLocalMoveY(_startPoint, _durationAnimation))
             .SetLoops(-1, LoopType.Yoyo);
    }

    public void StartAnimation()
    {
        /*  DOTween.Sequence()
             .Append(transform.DOMove(_endVector, _durationAnimation))
             .AppendInterval(_delay)
             .Append(transform.DOMove(_startVector, _durationAnimation))
             .SetLoops(-1); */
    }

    public void RestartAnimation()
    {
        _tween.Kill();
        transform.position = _startVector;
        Start();
    }
}
