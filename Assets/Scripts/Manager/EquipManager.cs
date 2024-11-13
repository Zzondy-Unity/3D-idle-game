using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public IWeapon Weapon { get; private set; }

    public void SetWeapon(IWeapon weapon)
    {
        this.Weapon = weapon;
    }

    public void EquipItem(WeaponSO weaponData)
    {
        
    }
}
