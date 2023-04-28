using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RecipeUI : MonoBehaviour
{
    [SerializeField] RecipeData recipe;

    [SerializeField] TMP_Text recipeNameText;
    [SerializeField] TMP_Text description;
    [SerializeField] List<IngredientSlot> ingredientSlots ;
    [SerializeField] Button craftButton;
    // Start is called before the first frame update
    void Start()
    {
        recipeNameText.text = recipe.recipeName;
        description.text = recipe.description;
        int index = 0;
        foreach (var ingredient in recipe.ingredients)
        {
            var slot = ingredientSlots.ElementAt(index);
            slot.image.sprite = ingredient.Item.icon;
            slot.quantity.text = ingredient.quantity.ToString();
            index++;
        }

        craftButton.onClick.AddListener(CraftRecipe);
    }

    private void OnDestroy() => craftButton.onClick.RemoveListener(CraftRecipe);

    void CraftRecipe()
    {
        foreach(var ingredient in recipe.ingredients)
        {
            for (int i = 0; i < ingredient.quantity; i++)
                Inventory.instance.RemoveItem(ingredient.Item);
        }

        Inventory.instance.AddItem(recipe.result);
    }
}

[System.Serializable]
public struct IngredientSlot
{
    public Image image;
    public TMP_Text quantity;
}
