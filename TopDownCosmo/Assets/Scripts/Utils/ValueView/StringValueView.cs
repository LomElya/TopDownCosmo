using UnityEngine.UI;
using UnityEngine;

public class StringValueView : ValueView<string>
{
    [SerializeField] private string _template;

    public override void Show(string value)
    {
        gameObject.SetActive(true);
        _text.text = $"{_template}{value}";
    }
}
