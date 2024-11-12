using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCondition : MonoBehaviour
{
    public UICondition uiCondition;

    private ConditionBar HP { get {  return uiCondition.HP; } }
    private ConditionBar MP { get { return uiCondition.MP; } }


    private void Start()
    {
        CharacterManager.Instance.Player.HealthSystem.OnDamage += OnHPChanged;
        CharacterManager.Instance.Player.HealthSystem.OnHeal += OnHPChanged;
        EventBus.Subscribe(EventType.LevelUp, OnLevelUP);
    }

    private void UIUpdate()
    {
        HP.UpdateConditionBar();
    }

    private void OnHPChanged()
    {
        HP.curValue = CharacterManager.Instance.Player.HealthSystem.CurrentHealth;
        Debug.Log("hp Changed. curHealth : " + HP.curValue);
        EventBus.Publish(EventType.PlayerHPChanged);
        UIUpdate();
    }

    private void OnLevelUP()
    {
        HP.MaxValueChange(50);
        HP.curValue = HP.maxValue;
        EventBus.Publish(EventType.PlayerHPChanged);
        UIUpdate();
    }
}
