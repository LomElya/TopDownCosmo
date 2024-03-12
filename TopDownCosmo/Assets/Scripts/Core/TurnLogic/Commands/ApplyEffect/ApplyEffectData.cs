public class ApplyEffectData : CommandData
{
    public EffectModel EffectModel { get; private set; }
    public ITriggerable Triggerable { get; private set; }

    public ApplyEffectData(EffectModel effectModel, ITriggerable triggerable)
    {
        EffectModel = effectModel;
        Triggerable = triggerable;
    }
}
