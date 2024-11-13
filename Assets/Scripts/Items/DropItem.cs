using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public ItemSO itemData;

    public float speed = 1f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            CharacterManager.Instance.Player.AddItem?.Invoke(itemData);
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
