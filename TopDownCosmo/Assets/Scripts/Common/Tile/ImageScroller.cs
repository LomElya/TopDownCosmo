using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(RawImage))]
public class ImageScroller : MonoBehaviour
{
    [SerializeField] protected RawImage _image;

    [SerializeField, Range(-1, 10)] private float _parallaxCoefficient = 1f;

    [SerializeField, Range(-1, 1)] private float _yDirection = 1f;

      private void OnValidate() =>
          _image ??= GetComponent<RawImage>();

    public void Scrolling(float speedMove)
    {
        speedMove *= _parallaxCoefficient;

        _image.uvRect = new Rect(_image.uvRect.position +
                  new Vector2(0f, _yDirection * speedMove) * Time.deltaTime,
                  _image.uvRect.size);
    }
}
