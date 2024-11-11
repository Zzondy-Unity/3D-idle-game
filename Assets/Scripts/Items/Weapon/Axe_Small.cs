using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_Small : MonoBehaviour, IWeapon
{
    public WeaponSO WeaponData;

    public float GetWeaponRange()
    {
        return WeaponData.AttackRange;
    }

}
