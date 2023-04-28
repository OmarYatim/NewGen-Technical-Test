using System;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;
    public ItemData[] inventory = new ItemData[7];
    public static Action onInventoryChanged;
    private void Awake() => instance = this;
    public void AddItem(ItemData item)
    {
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] != null && inventory[i] == item)
            {
                inventory[i].quantity++;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
                return;
            }
        }

        // If the item is not in the inventory, add it to the inventory with a quantity of 1
        for (int i = 0; i < inventory.Length; i++)
        {
            if (inventory[i] == null)
            {
                item.quantity = 1;
                inventory[i] = item;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
                return;
            }
        }
    }

    public void RemoveItem(ItemData item)
    {
        if (inventory.Contains(item))
        {
            // If the item is in the inventory and has a quantity of more than 1, decrease the quantity
            int index = Array.IndexOf(inventory,item);
            if (inventory[index].quantity > 1)
            {
                inventory[index].quantity--;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
            }
            // If the item is in the inventory and has a quantity of 1, remove it from the inventory
            else
            {
                inventory[index] = null;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
            }
        }
    }

    public void RemoveItem(ItemType itemType)
    {
        if (HasItem(itemType))
        {
            // If the item is in the inventory and has a quantity of more than 1, decrease the quantity
            var item = inventory.FirstOrDefault(x => x != null && x.itemType == itemType);
            int index = Array.IndexOf(inventory, item);
            if (inventory[index].quantity > 1)
            {
                inventory[index].quantity--;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
            }
            // If the item is in the inventory and has a quantity of 1, remove it from the inventory
            else
            {
                inventory[index] = null;
                if (onInventoryChanged != null)
                    onInventoryChanged.Invoke();
            }
        }
    }

    public bool HasItem(ItemData item) => inventory.Contains(item);
    public bool HasItem(ItemType itemType) => inventory.Any(item => item != null && item.itemType == itemType);
    

    public int GetItemQuantity(ItemData item)
    {
        if (inventory.Contains(item))
        {
            int index = Array.IndexOf(inventory, item);
            return inventory[index].quantity;
        }
        
        return 0;
    }
}
