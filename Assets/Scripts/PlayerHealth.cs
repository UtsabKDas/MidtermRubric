using System;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    public bool IsDead;
    [SerializeField] private int maxHealth = 100;
    private int currentHealth;
   //public event Action OnDamaged;
   //public event Action OnDeath;

    [SerializeField] private HUDManager hud;

    private void Start()
    {
        currentHealth = maxHealth;

        if (hud == null)
        {
            hud = GameManager.Instance.HUDManager;
        }
        hud.UpdateHealthBar(currentHealth / maxHealth);
    }

    public void TakeDamage(int amount)
    {
        if (IsDead)
        {
            return;
        }
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            currentHealth = 0;
            IsDead = true;
            GameManager.Instance.TriggerLose();
        }
        hud.UpdateHealthBar((float)currentHealth / (float)maxHealth);
    }
}