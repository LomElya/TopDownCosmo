using System;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

[RequireComponent(typeof(Text))]
public class LevelView : MonoBehaviour
{
    [SerializeField] private string _template;
    private Text _value;

    private Level _level;

    private void OnValidate() =>
        _value ??= GetComponent<Text>();

    [Inject]
    private void Construct(Level level)
    {
        _level = level;

        UpdateLevel(_level.GetCurrentLevelID());

        _level.LevelChange += UpdateLevel;
    }

    private void UpdateLevel(int id) => _value.text = $"{_template}{id + 1}";

    private void OnDisable() => _level.LevelChange -= UpdateLevel;
}
