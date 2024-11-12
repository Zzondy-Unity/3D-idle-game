using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UICondition : MonoBehaviour
{
    public ConditionBar HP;
    public ConditionBar MP;


    private void Start()
    {
        CharacterManager.Instance.Player.condition.uiCondition = this;

        HP.maxValue = CharacterManager.Instance.Player.PlayerData.MaxHP;
        MP.maxValue = CharacterManager.Instance.Player.PlayerData.MaxMP;
    }
}
