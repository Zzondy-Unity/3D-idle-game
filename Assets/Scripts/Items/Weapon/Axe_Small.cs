using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_Small : MonoBehaviour, IWeapon
{
    [SerializeField] private BoxCollider weaponCollier;

    [field: SerializeField] public WeaponSO WeaponData { get; private set; }

    private float TotalDamage = 10f;

    private HashSet<Collider> hitTargets = new HashSet<Collider>();

    private void Start()
    {
        weaponCollier.enabled = false;
    }

    public void Attack()
    {
        hitTargets.Clear();
        TotalDamage = WeaponData.WeaponDamage + CharacterManager.Instance.Player.PlayerData.AttackData.Damage * 1.2f;
    }


    public float GetWeaponRange()
    {
        return WeaponData.AttackRange;
    }

    public void ToggleWeaponCollider(bool state)
    {
        weaponCollier.enabled = state;
    }


    private void OnTriggerEnter(Collider other)
    {
        if (hitTargets.Contains(other)) return;

        if (other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            if (health.ChangeHealth(-TotalDamage))
            {
                hitTargets.Add(other);
            }
        }
    }
}
