using System.Collections.Generic;
using System;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

[System.Serializable]
public struct Slot
{
    public Image SlotImage;
    public TMP_Text QuantityText;
}
public class InventoryUI : MonoBehaviour
{
    [SerializeField] Slot[] slots;
    // Start is called before the first frame update
    void Start() => Inventory.onInventoryChanged += OnInventoryChanged;
    void OnDestroy() => Inventory.onInventoryChanged -= OnInventoryChanged;

    private void OnInventoryChanged()
    {
        for (int i = 0; i < Inventory.instance.inventory.Length; i++)
        {
            var item = Inventory.instance.inventory[i];
            if (item != null)
            { 
                slots[i].SlotImage.sprite = item.icon;
                slots[i].SlotImage.color = Color.white;
                slots[i].QuantityText.text= item.quantity.ToString();
            }
            else
            {
                slots[i].SlotImage.sprite = null;
                slots[i].SlotImage.color = new Color(1,1,1,0);
                slots[i].QuantityText.text = "";
            }
        }
    }
}
