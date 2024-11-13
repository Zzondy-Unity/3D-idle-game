using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = ("DefaultWeaponSO"), menuName = ("Items/Weapon"))]
public class WeaponSO : ItemSO
{
    //�⺻�������� ��� + ���ⵥ����
    [field: SerializeField] public float WeaponDamage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; } = 10f;
}

//���Ÿ������ ��ӹ޴°ɷ�
