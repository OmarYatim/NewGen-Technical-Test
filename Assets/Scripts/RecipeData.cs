using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "RecipeData")]
public class RecipeData : ScriptableObject
{
    public string recipeName;
    public List<Ingredient> ingredients = new List<Ingredient>();
    public string description;
    public ItemData result;
}

[System.Serializable]
public struct Ingredient
{
    public ItemData Item;
    public int quantity;
}
