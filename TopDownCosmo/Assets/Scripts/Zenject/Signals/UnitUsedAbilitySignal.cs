using System.Collections.Generic;

public class UnitUsedAbilitySignal
{
    public readonly ITriggerable Owner;
    public readonly ITriggerable Target;
    public readonly List<EffectModel> Effects;

    public UnitUsedAbilitySignal(ITriggerable owner, ITriggerable target, List<EffectModel> effects)
    {
        Owner = owner;
        Target = target;
        Effects = effects;
    }
}
