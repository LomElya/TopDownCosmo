public struct ShipStats
{
    public float MaxHealth;
    public float Speed;
    public bool IsImmortal;

    public ShipStats(float maxHealth, float speed, bool isImmortal = false)
    {
        MaxHealth = maxHealth;
        Speed = speed;
        IsImmortal = isImmortal;
    }
}
