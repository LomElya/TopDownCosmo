public class HealEffect : IEffect
{
    private readonly float _healValue;

    private ITriggerable _triggerable;

    public HealEffect(float healValue) => _healValue = healValue;

    public void Assign(ITriggerable triggerable)
    {
        _triggerable = triggerable;

        if (_triggerable.Owner.TryGetComponent(out IHealable healable))
        {
            healable.OnTakeHealth(_healValue);

            Remove();
        }
    }

    public virtual void Remove() { }
}
