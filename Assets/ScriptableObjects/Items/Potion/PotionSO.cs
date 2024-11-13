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
                CharacterManager.Instance.Player.HealthSystem.ChangeHealth(value);  //Player�� StatHandler ����Ѱ� ����� �������� �����ϴ� �Լ� ���ۿ���
                break;
            case PotionType.Attack:
                CharacterManager.Instance.Player.HealthSystem.ChangeHealth(value);
                break;
        }
    }
}
