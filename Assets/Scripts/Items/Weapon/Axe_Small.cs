using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_Small : MonoBehaviour, IWeapon
{
    [SerializeField] private Collider myCollider;

    [field: SerializeField] public WeaponSO WeaponData { get; private set; }

    //플레이어의 데미지까지해서 로직추가
    private float TotalDamage => WeaponData.WeaponDamage;

    public void Attack()
    {

    }


    public float GetWeaponRange()
    {
        return WeaponData.AttackRange;
    }

    public void ToggleWeaponCollider(bool state)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = state;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == myCollider) return;

        if(other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            if (health.ChangeHealth(-TotalDamage))
            {
                //데미지 주었으니 이펙트를 켜줌
            }
        }
    }
}
