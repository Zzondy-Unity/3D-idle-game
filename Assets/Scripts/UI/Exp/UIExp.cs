using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIExp : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI LevelTxt;
    [SerializeField] private Slider expSlide;
    private int EnemyDeadCount;

    public int Level
    {
        get { return EnemyDeadCount / 10; }
    }

    private void Start()
    {
        EventBus.Subscribe(EventType.EnemyDead, OnGetEXP);
        expSlide.value = 0;
        LevelTxt.text = string.Empty;
    }

    private void OnGetEXP()
    {
        EnemyDeadCount++;
        expSlide.value = (EnemyDeadCount % 10) * 0.1f;

        if(EnemyDeadCount % 10 == 0)
        {
            EventBus.Publish(EventType.LevelUp);
        }

        LevelTxt.text = "LV. " + Level.ToString();
    }
}
