using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public float moveSpeed = 1f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.Instance.curGold++;
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Magnetic"))
        {
            if(TryGetComponent<Rigidbody>(out Rigidbody rb))
            {
                Vector3 dir = (other.transform.position - transform.position).normalized;
                transform.Translate(dir * moveSpeed * Time.deltaTime);
            }
        }
    }
}
