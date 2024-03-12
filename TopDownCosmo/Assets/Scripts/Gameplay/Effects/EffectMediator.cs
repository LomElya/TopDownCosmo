public class EffectMediator 
{
    private readonly ITriggerable _triggerable;
    private readonly GameplayObject _unitModel;
    private IEffect _currentEffect;

    public EffectMediator(ITriggerable triggerable)
    {
        _triggerable = triggerable;
    }

    public void Replace(IEffect effect)
    {
        // _currentEffect?.Remove();
        _currentEffect = effect;
        _currentEffect.Assign(_triggerable);
    }

    public void Dispose()
    {
        _currentEffect?.Remove();
    }
}
