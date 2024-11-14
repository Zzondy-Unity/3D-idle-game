using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PotionType
{
    HP,
    Speed,
    Attack
}

[CreateAssetMenu(fileName = ("DefaultPotion"), menuName = ("Items/Potion"))]

public class PotionSO : ItemSO
{
    public float value;
    public PotionType type;
}
