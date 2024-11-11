using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_Small : MonoBehaviour, IWeapon
{
    [SerializeField] private Collider myCollider;

    [field: SerializeField] public WeaponSO WeaponData { get; private set; }

    private float TotalDamage => WeaponData.WeaponDamage;

    public void Attack()
    {
        
    }

    public float GetWeaponRange()
    {
        return WeaponData.AttackRange;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            if (health.ChangeHealth(TotalDamage))
            {
                //데미지 주었으니 이펙트를 켜줌
            }
        }
    }
}
