public static class EffectExtensions
{
    public static ITriggerable GetTarget(this TargetType type, ITriggerable owner, ITriggerable target)
    {
        switch (type)
        {
            case TargetType.Enemy:
                return target;

            case TargetType.Self:
                return owner;

            default:
                return target;
        }
    }

    public static IEffect GetEffect(this EffectType effectType, EffectModel effect)
    {
        switch (effectType)
        {
            case EffectType.Attack:
                return new DamageEffect(effect.Value);

            case EffectType.Gold:
                return new AddGoldEffect((int)effect.Value);

            case EffectType.Heal:
                return new HealEffect(effect.Value);

            case EffectType.Shield:
                return new ShieldEffect(effect.Value);

            default:
                return new EmptyEffect();
        }
    }
}
