using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConditionBar : MonoBehaviour
{
    public float maxValue;
    public float curValue;
    public Image ConditionUI;

    private void Start()
    {
        curValue = maxValue;
        UpdateConditionBar();
    }

    public void UpdateConditionBar()
    {
        ConditionUI.fillAmount = GetPercentage();
    }

    public float GetPercentage()
    {
        return curValue / maxValue;
    }

    public void Add(float amount)
    {
        curValue = Mathf.Clamp(curValue += amount, 0, maxValue);
    }

    public void Subtract(float amount)
    {
        curValue = Mathf.Clamp(curValue -= amount, 0, maxValue);
    }

    public void MaxValueChange(float amount)
    {
        maxValue = Mathf.Max(0, maxValue + amount);
    }
}
