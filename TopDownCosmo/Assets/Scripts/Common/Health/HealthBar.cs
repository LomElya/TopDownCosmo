using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Zenject;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private HealthView _prefabHealth;
    [SerializeField] private Transform _layoutGroup;

    private List<HealthView> _hearts = new();

    private ShipSpawner _spawner;
    private Health _health;

    private int _maxHealth => _hearts.Count;

    [Inject]
    private void Construct(ShipSpawner spawner)
    {
        //_ship = ship;
        _spawner = spawner;

        _spawner.Spawned += OnSpawn;
    }

    private void OnSpawn(Ship ship)
    {
        Clear();

        _health = ship.Health;

        for (int i = 0; i < _health.MaxHealth; i++)
        {
            var newHeart = Instantiate(_prefabHealth, _layoutGroup);
            _hearts.Add(newHeart);
        }

        _health.ChangeHealth += DisplayHealth;
    }

    private void DisplayHealth(float value)
    {
        int index = (int)value;
        float percent = value / _maxHealth;

        for (int i = 0; i < _hearts.Count(); i++)
        {
            _hearts[i].ChangeFill(1f);

            if (i == index)
            {
                _hearts[i].ChangeFill(CalculateHeart(value));
            }

            if (i > index)
            {
                _hearts[i].ChangeFill(0f);
            }
        }
    }

    private void Clear()
    {
        foreach (var heart in _hearts)
            heart.Remove();

        _hearts.Clear();
    }

    private float CalculateHeart(float value)
    {
        string str = value.ToString();

        if (str.Count() < 3)
            return 0f;

        string result = "0,";
        int number = 1;

        foreach (char symbol in str)
        {
            if (number >= 3)
                result += symbol;

            number++;
        }

        return float.Parse(result);
    }

    private void OnDestroy()
    {
        _spawner.Spawned -= OnSpawn;
        _health.ChangeHealth -= DisplayHealth;
    }
}
