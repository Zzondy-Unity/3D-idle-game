using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (fileName = ("DefaultWeaponSO"), menuName = ("Items/Weapon"))]
public class WeaponSO : ScriptableObject
{
    [field: SerializeField] public float AttackRange { get; private set; } = 10f;
}
