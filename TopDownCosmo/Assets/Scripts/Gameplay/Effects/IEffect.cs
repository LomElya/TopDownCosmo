public interface IEffect
{
    void Assign(ITriggerable triggerable);
    void Remove();
}
