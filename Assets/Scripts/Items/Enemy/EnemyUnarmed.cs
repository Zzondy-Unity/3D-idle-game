using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnarmed : MonoBehaviour, IWeapon
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
        if (other == myCollider) return;

        if (other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            Debug.Log($"{health.gameObject.name} 뽑아냄");
            if (health.ChangeHealth(-TotalDamage))
            {
                Debug.Log("데미지 주는데 성공함");
                //데미지 주었으니 이펙트를 켜줌
            }
        }
    }

    public void ToggleWeaponCollider(bool state)
    {
        Collider collider = GetComponent<Collider>();
        collider.enabled = state;
    }
}
