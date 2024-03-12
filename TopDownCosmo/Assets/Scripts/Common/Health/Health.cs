using System;
using UnityEngine;

public class Health : MonoBehaviour, IDamageable, IHealable, IShielded
{
    public event Action<float> ChangeHealth;
    public event Action Die;

    [SerializeField] private GameObject _shieldObject;

    public float MaxHealth { get; private set; }
    public float CurrentHealth { get; private set; }

    public GameplayObject Owner { get; private set; }

    public EffectMediator EffectMediator { get; private set; }

    private bool _canTakeDamage = true;

    public void Init(GameplayObject owner, float maxHealth)
    {
        Owner = owner;

        MaxHealth = maxHealth;
        CurrentHealth = MaxHealth;

        ChangeShield(false);

        EffectMediator = new EffectMediator(this);
    }

    public void OnTakeDamage(float value)
    {
        if (!_canTakeDamage)
            return;

        CurrentHealth -= value;

        IsLive();

        ChangeHealth?.Invoke(CurrentHealth);
    }

    public void OnTakeHealth(float value)
    {
        CurrentHealth += value;

        if (CurrentHealth > MaxHealth)
            CurrentHealth = MaxHealth;

        ChangeHealth?.Invoke(CurrentHealth);
    }

    public void Kill()
    {
        OnTakeDamage(MaxHealth);
    }

    public void ChangeShield(bool isShielded)
    {
        _shieldObject.gameObject.SetActive(isShielded);
        _canTakeDamage = !isShielded;
    }

    private bool IsLive()
    {
        if (CurrentHealth <= 0f)
        {
            CurrentHealth = 0f;
            Die?.Invoke();
            return false;
        }

        return true;
    }
}
