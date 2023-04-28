using TMPro;
using UnityEngine;

public class PickupItem : MonoBehaviour, IInteractable
{
    [SerializeField] private Animator playerAnimator;
    [SerializeField] private ItemData itemData;
    [SerializeField] float pickupCooldown = 1f;
    [SerializeField] GameObject text;
    float timeSincePickUP = 0;

    public void Interact()
    {
        if (timeSincePickUP < pickupCooldown)
            return;

        if (playerAnimator != null) playerAnimator.SetTrigger("PickUP");
        Inventory.instance.AddItem(itemData);
        timeSincePickUP = 0;
    }

    void Update()
    {
        timeSincePickUP += Time.deltaTime;  
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
            text.SetActive(true);
    }
    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
            text.SetActive(false);
    }
}
