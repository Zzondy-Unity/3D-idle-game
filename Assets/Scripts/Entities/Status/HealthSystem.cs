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
    public event Action OnInvincibilityEnd;

    private bool isAttacked = false;


    private void Start()
    {
        CurrentHealth = data.MaxHP;
        MaxHealth = data.MaxHP;
    }

    private void Update()
    {
        if(isAttacked && LastChanged < healthChangeDelay)
        {
            LastChanged += Time.deltaTime;
            if(LastChanged >= healthChangeDelay)
            {
                OnInvincibilityEnd?.Invoke();
                isAttacked = true;
            }
        }
    }

    public bool ChangeHealth(float amount)
    {
        if (LastChanged < healthChangeDelay) return false;

        LastChanged = 0f;
        CurrentHealth += amount;
        CurrentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);

        if (CurrentHealth <= 0)
        {
            Debug.Log($"{gameObject.name}이 죽음");
            OnDeath?.Invoke();
            return true;
        }
        if(amount > 0)
        {
            OnHeal?.Invoke();
        }
        else
        {
            Debug.Log($"{gameObject.name}이 {amount}의 데미지를 입음");
            OnDamage?.Invoke();
            isAttacked = true;
        }
        return true;
    }
}
