public class DamageEffect : IEffect
{
    private readonly float _damageValue;

    private ITriggerable _triggerable;

    public DamageEffect(float damageValue) => _damageValue = damageValue;

    public void Assign(ITriggerable triggerable)
    {
        _triggerable = triggerable;

        if (_triggerable.Owner.TryGetComponent(out IDamageable damageable))
        {
            damageable.OnTakeDamage(_damageValue);

            Remove();
        }
    }

    public virtual void Remove() { }
}
