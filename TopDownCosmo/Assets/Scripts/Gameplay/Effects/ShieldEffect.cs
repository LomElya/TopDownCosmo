using System;
using System.Threading;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

public class ShieldEffect : IEffect
{
    private readonly float _timeShield;

    private ITriggerable _triggerable;
    private CancellationTokenSource _cancellationTokenSource;

    public ShieldEffect(float timeShield) => _timeShield = timeShield;

    public void Assign(ITriggerable triggerable)
    {
        _triggerable = triggerable;

        if (_triggerable.Owner.TryGetComponent(out IShielded shielded))
        {
            shielded.ChangeShield(true);

            _cancellationTokenSource = new CancellationTokenSource();

            ShieldTask(_cancellationTokenSource.Token, shielded).Forget();
        }
    }

    public void Remove()
    {
        _cancellationTokenSource?.Cancel();
    }

    private async UniTask ShieldTask(CancellationToken cancellationToken, IShielded shielded)
    {
        await Task.Delay(TimeSpan.FromSeconds(_timeShield), cancellationToken);
        shielded.ChangeShield(false);
    }
}
