using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float health = 100;
    [SerializeField] float maxHealth = 100;
    public bool damageEnabled = true;

    [SerializeField] private Scrollbar healthBar;
    [SerializeField] private EndGameUI EndGamePanel;
    public void TakeDamage(int damage)
    {
        if (!damageEnabled) return;
        health -= damage;
        healthBar.size = health / maxHealth; 
        if (health <= 0)
        {
            GetComponent<Animator>().SetTrigger("isDead");
            GameManager.Instance.playerIsDead = true;
            EndGamePanel.GameLost();
        }
    }

    public void IncreaseHealth(InputAction.CallbackContext context)
    {
        if (!Inventory.instance.HasItem(ItemType.PurplePotion)) return;

        health += 20f;
        if (health > maxHealth)
        {
            health = maxHealth;
        }
        healthBar.size = health / maxHealth;

        Inventory.instance.RemoveItem(ItemType.PurplePotion);
    }
}
