using UnityEngine;
using Zenject;

public class Ship : GameplayObject, IMovable
{
    public ShipStats BaseStats { get; private set; }
    public ShipStats CurrentStats { get; private set; }

    [field: SerializeField] public Health Health { get; private set; }
    [field: SerializeField] public WalletHolder WalletHolder { get; private set; }

    public float Speed => CurrentStats.Speed;
    public Transform Transform => transform;

    private Wallet _wallet;

    private SignalBus _signalBus;

    [Inject]
    private void Construct(Wallet wallet, SignalBus signalBus)
    {
        _wallet = wallet;
        _signalBus = signalBus;

        WalletHolder.Init(Owner, _wallet);
        Health.Init(Owner, CurrentStats.MaxHealth);

        Health.Die += OnDie;
    }

    public void Init(ShipStats stats)
    {
        Init();

        BaseStats = stats;
        CurrentStats = stats;
    }

    public void Move(Vector2 direction)
    {
        Transform.Translate(direction * Time.deltaTime * Speed);
    }

    public void Remove() => this.gameObject.SetActive(false);

    private void OnDie()
    {
        _signalBus.Fire(new PlayerDeadSignal());

        Remove();
    }

    private void OnDestroy()
    {
        Health.Die -= OnDie;
    }
}
