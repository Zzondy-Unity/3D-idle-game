using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : ConsumableItem
{
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
}
