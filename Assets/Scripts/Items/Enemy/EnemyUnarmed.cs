using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnarmed : MonoBehaviour, IWeapon
{
    [SerializeField] private Collider myCollider;
    Collider weaponCollider;

    [field: SerializeField] public WeaponSO WeaponData { get; private set; }
    private float TotalDamage => WeaponData.WeaponDamage;

    private void Awake()
    {
        weaponCollider = GetComponent<Collider>();
    }

    public void Attack()
    {
        
    }

    public float GetWeaponRange()
    {
        return WeaponData.AttackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if (other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            if (health.ChangeHealth(-TotalDamage))
            {
                //������ �־����� ����Ʈ�� ����
            }
        }
    }

    public void ToggleWeaponCollider(bool state)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = state;
    }
}
