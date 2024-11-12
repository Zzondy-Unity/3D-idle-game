using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe_Small : MonoBehaviour, IWeapon
{
    [SerializeField] private Collider myCollider;
    private Collider weaponCollier;

    [field: SerializeField] public WeaponSO WeaponData { get; private set; }

    //�÷��̾��� �����������ؼ� �����߰�
    private float TotalDamage => WeaponData.WeaponDamage + CharacterManager.Instance.Player.PlayerData.AttackData.Damage * 1.2f;

    public bool ColliderEnalbed => weaponCollier.enabled;

    private HashSet<Collider> hitTargets = new HashSet<Collider>();

    private void Start()
    {
        weaponCollier = GetComponent<Collider>();
    }

    public void Attack()
    {
        hitTargets.Clear();
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
        if (other == myCollider || hitTargets.Contains(other)) return;

        if (other.TryGetComponent<HealthSystem>(out HealthSystem health))
        {
            if (health.ChangeHealth(-TotalDamage))
            {
                Debug.Log($"{other.name}���� {TotalDamage}��ŭ�� �������� �־����ϴ�.");
                hitTargets.Add(other);
            }
        }
    }
}
