using UnityEngine;

public class ImagePlacement : MonoBehaviour
{
    private const string RenderLayer = "ImageRender";

    [SerializeField] private ShopImageAnimation _animation;

    [SerializeField] private SpriteRenderer _sprite;

    public void InstantiateModel(Sprite sprite)
    {
        _sprite.sprite = sprite;
        _animation.RestartAnimation();
    }
}
