using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class ScoreView : MonoBehaviour
{
    [SerializeField] private string _template;
    private Text _value;

    private Score _score;

    private void OnValidate() =>
        _value ??= GetComponent<Text>();

    [Inject]
    private void Construct(Score score)
    {
        _score = score;

        UpdateValue(_score.GetCurrentScore());

        _score.ScoreChange += UpdateValue;
    }

    public void UpdateValue(int value) => _value.text = $"{_template}{value}";

    private void OnDestroy() => _score.ScoreChange -= UpdateValue;
}
