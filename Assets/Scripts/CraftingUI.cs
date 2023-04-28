using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingUI : MonoBehaviour
{
    [SerializeField] private RecipeData[] recipes;
    [SerializeField] private Button[] buttons;

    private void Awake()
    {
        CraftingTable.OnCraftingUIOpened += UpdateCraftingUI;
        Inventory.onInventoryChanged += UpdateCraftingUI;
    }

    private void OnDestroy()
    {
        CraftingTable.OnCraftingUIOpened -= UpdateCraftingUI;
        Inventory.onInventoryChanged -= UpdateCraftingUI;
    }
    public bool CheckRecipeIngredients(RecipeData recipe)
    {
        foreach (var ingredient in recipe.ingredients)
        {
            if (Inventory.instance.HasItem(ingredient.Item) && Inventory.instance.GetItemQuantity(ingredient.Item) >= ingredient.quantity)
            {
                continue;
            }
            else
            {
                return false;
            }
        }
        return true;
    }

    private void UpdateCraftingUI()
    {
        for (int i = 0; i < recipes.Length; i++)
        {
            bool canCraft = CheckRecipeIngredients(recipes[i]);

            if (canCraft)
            {
                // Enable the button if the player can craft the recipe.
                buttons[i].interactable = true;
            }
            else
            {
                // Disable the button if the player can't craft the recipe.
                buttons[i].interactable = false;
            }
        }
    }
}
