using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShield : MonoBehaviour
{
    [SerializeField] float shieldDuration = 5f;
    private bool shieldActive = false;
    public void GetShield(InputAction.CallbackContext context)
    {
        if (shieldActive) return;
        bool t = Inventory.instance.HasItem(ItemType.YellowPotion);
        if (Inventory.instance.HasItem(ItemType.YellowPotion))
        {
            Inventory.instance.RemoveItem(ItemType.YellowPotion);
            StartCoroutine(ActivateShield());
        }
    }

    IEnumerator ActivateShield()
    {
        shieldActive = true;
        // Play shield animation
        Debug.Log("Shield activated!");

        // Add shield logic here, for example:
        GetComponent<PlayerHealth>().damageEnabled = false;
        yield return new WaitForSeconds(shieldDuration);
        GetComponent<PlayerHealth>().damageEnabled = true;

        // Play shield animation
        Debug.Log("Shield deactivated!");
        shieldActive = false;
    }
}
