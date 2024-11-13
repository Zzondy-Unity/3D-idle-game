using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UICoin : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI coinTxt;
    private int Gold => GameManager.Instance.curGold;

    private void UIUpdate()
    {
        coinTxt.text = Gold.ToString() + "G";
    }

    private void Update()
    {
        UIUpdate();
    }
}
