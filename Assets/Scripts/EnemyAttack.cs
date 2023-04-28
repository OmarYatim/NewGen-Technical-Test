using System.Collections;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isDead = false;
    [SerializeField] int damage = 20;

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !isDead)
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
    }
}

