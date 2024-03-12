using System;
using UnityEngine;
using Zenject;
using Random = UnityEngine.Random;

public class InteractableSpawner
{
    public event Action<Interactable> InteractableActive;
    public event Action<Interactable> InteractableHidded;

    private const float _spawnPosition = Constants.Border.UP_POSITION + 2f;
    private const float _minPosX = Constants.Border.LEFT_POSITION + 0.5f;
    private const float _maxPosX = Constants.Border.RIGHT_POSITION - 0.5f;

    private InteractableFactory _factory;

    private SurviveScenarioProcessor _surviveScenarioProcessor;

    [Inject]
    private void Construct(InteractableFactory factory, SurviveScenarioProcessor surviveScenarioProcessor)
    {
        _factory = factory;
        _surviveScenarioProcessor = surviveScenarioProcessor;

        _surviveScenarioProcessor.InteractableSpawn += OnSpawn;
    }

    public void OnSpawn(InteractableType type)
    {
        Interactable interactable = _factory.Get(type);

        interactable.transform.localPosition = RandomSpawnPosition();

        InteractableActive?.Invoke(interactable);
    }

    public void Hide(Interactable interactable)
    {
        _factory.Hide(interactable);

        InteractableHidded?.Invoke(interactable);
    }

    private Vector2 RandomSpawnPosition()
    {
        float x = Random.Range(_minPosX, _maxPosX);

        return new Vector2(x, _spawnPosition);
    }

    private void OnDestroy()
    {
        _surviveScenarioProcessor.InteractableSpawn -= OnSpawn;
    }
}



