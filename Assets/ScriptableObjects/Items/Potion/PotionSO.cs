using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IUsable
{
    public void Use();
}

public enum PotionType
{
    HP,
    Speed,
    Attack
}

[CreateAssetMenu(fileName = ("DefaultPotion"), menuName = ("Items/Potion"))]

public class PotionSO : ItemSO, IUsable
{
    public float value;
    public PotionType type;
    public void Use()
    {
        switch (type)
        {
            case PotionType.HP:
                CharacterManager.Instance.Player.HealthSystem.ChangeHealth(value);
                break;
            case PotionType.Speed:
                CharacterManager.Instance.Player.HealthSystem.ChangeHealth(value);  //Player에 StatHandler 비슷한걸 만들어 버프등을 조절하는 함수 제작예정
                break;
            case PotionType.Attack:
                CharacterManager.Instance.Player.HealthSystem.ChangeHealth(value);
                break;
        }
    }
}
