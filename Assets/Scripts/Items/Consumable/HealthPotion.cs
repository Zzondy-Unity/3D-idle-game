using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : ConsumableItem
{
    public float speed = 1f;

    public override void Consume()  //인터페이스의 Use로 대체됨
    {
        CharacterManager.Instance.Player.HealthSystem.ChangeHealth(potionData.value);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.AddItem?.Invoke(potionData);
            Destroy(gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Magnetic"))
        {
            if (TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;
                transform.Translate(dir * speed * Time.deltaTime);
            }
        }
    }
}
