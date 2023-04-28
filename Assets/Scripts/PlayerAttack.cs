using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        { 
            other.GetComponent<AIBrain>().SwitchToDeathState();
            GameManager.Instance.NumberOfEnemies--;
        }
    }
}
