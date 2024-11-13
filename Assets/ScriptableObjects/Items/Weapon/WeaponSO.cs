using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = ("DefaultWeaponSO"), menuName = ("Items/Weapon"))]
public class WeaponSO : ItemSO
{
    //기본데미지의 배수 + 무기데미지
    [field: SerializeField] public float WeaponDamage { get; private set; }
    [field: SerializeField] public float AttackDelay { get; private set; }
    [field: SerializeField] public float AttackRange { get; private set; } = 10f;
}

//원거리무기는 상속받는걸로
