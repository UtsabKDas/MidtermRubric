using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead;
    [SerializeField] private int maxHealth = 100;
    public int CurrentHealth { get;  set; }
    public event Action<int, int> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField] private HUDManager hud;

    private void Start()
    {
        if (hud == null)
        {
            hud = GameManager.Instance.HUDManager;
        }
        OnHealthChanged += hud.UpdateHealthBar;
        OnHealthChanged += GameManager.Instance.OtherOnHealthChangedSubscriber;

        CurrentHealth = maxHealth;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        OnDeath += GameManager.Instance.TriggerLose;
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
        {
            return;
        }
        CurrentHealth -= amount;
        OnHealthChanged?.Invoke(CurrentHealth, maxHealth);

        if (CurrentHealth <= 0)
        {
            CurrentHealth = 0;
            IsDead = true;
            OnDeath?.Invoke();
        }
    }
}