using UnityEngine;

[CreateAssetMenu(fileName = "ItemData")]
public class ItemData : ScriptableObject
{
    [HideInInspector] public string itemName => itemType.ToString();
    public Sprite icon;
    public ItemType itemType;
    [HideInInspector] public int quantity = 0;
}

public enum ItemType
{
    YellowMushroom,
    PurpleMushroom,
    Sunflower,
    Hyacinth,
    YellowPotion,
    PurplePotion
}