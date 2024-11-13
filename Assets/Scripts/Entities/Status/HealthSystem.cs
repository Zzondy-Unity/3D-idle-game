using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthSystem : MonoBehaviour
{
    public EntitySO data;
    public float CurrentHealth {  get; private set; }
    public float MaxHealth { get; private set; }

    [SerializeField] private float healthChangeDelay = 0.5f;
    private float LastChanged = float.MaxValue;

    public event Action OnDamage;
    public event Action OnHeal;
    public event Action OnDeath;

    private bool isAttacked = false;


    private void Start()
    {
        MaxHealth = data.MaxHP;
        CurrentHealth = MaxHealth;
    }

    private void Update()
    {
        if (isAttacked)
        {
            LastChanged += Time.deltaTime;
            if (LastChanged >= healthChangeDelay)
            {
                isAttacked = false;
            }
        }
    }

    public bool ChangeHealth(float amount)
    {
        if (isAttacked && LastChanged < healthChangeDelay) return false;

        LastChanged = 0f;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            OnDeath?.Invoke();
            return true;
        }
        if(amount > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            OnDamage?.Invoke();
            isAttacked = true;
        }
        return true;
    }
}
