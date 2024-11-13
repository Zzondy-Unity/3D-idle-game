using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipManager : Singleton<EquipManager>
{
    public WeaponSO defaultWeapon;
    public WeaponSO[] allWeapons;
    public Transform weaponJoint;

    private string path = "Weapon/";
    private Dictionary<int, string> weaponDictionary = new Dictionary<int, string>();

    public IWeapon Weapon { get; private set; }

    public void SetWeapon(IWeapon weapon)
    {
        this.Weapon = weapon;
    }

    private void Start()
    {
        for (int i = 0; i < allWeapons.Length; i++)
        {
            weaponDictionary.Add(allWeapons[i].id, allWeapons[i].PrefabName);
        }

        CharacterManager.Instance.Player.AddItem?.Invoke(defaultWeapon);
        EquipItem(defaultWeapon);
    }

    public void EquipItem(WeaponSO weaponData)
    {
        int itemIndex = weaponData.id;
        string prefabPath = path + weaponDictionary[itemIndex];
        GameObject newWeapon = Resources.Load<GameObject>(prefabPath);

        Instantiate(newWeapon, weaponJoint);

        IWeapon newIWeapon = newWeapon.GetComponent<IWeapon>();
        SetWeapon(newIWeapon);
    }
}
