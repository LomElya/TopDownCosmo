using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Interactable : GameplayObject, ITriggerable
{
    public event Action<Interactable> Interacted;

    [SerializeField] protected InteractTrigger _trigger;

    private List<EffectModel> _effects;

    public EffectMediator EffectMediator { get; private set; }

    private SignalBus _signalBus;

    [Inject]
    private void Construct(SignalBus signalBus) =>
        _signalBus = signalBus;


    public void SetEffects(List<EffectModel> effects)
    {
        _effects = effects;
    }

    public override void Init()
    {
        base.Init();

        EffectMediator = new EffectMediator(this);

        _trigger.Enter += OnEnterTrigger;
    }

    public void Interact(ITriggerable trigger)
    {
        _signalBus.Fire(new UnitUsedAbilitySignal(this, trigger, _effects));
    }

    public void Hide()
    {
        Interacted?.Invoke(this);
        _trigger.Enter -= OnEnterTrigger;
    }

    private void OnEnterTrigger(ITriggerable trigger)
    {
        Interact(trigger);
        Hide();
    }

    private void OnDestroy()
    {
        _trigger.Enter -= OnEnterTrigger;
    }
}