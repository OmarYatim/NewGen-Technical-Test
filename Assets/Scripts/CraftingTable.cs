using System;
using UnityEngine;
using UnityEngine.InputSystem;


public class CraftingTable : MonoBehaviour, IInteractable
{
    [SerializeField] private GameObject CraftingPanel;

    public static Action OnCraftingUIOpened;
    public void Interact()
    { 
        CraftingPanel.SetActive(true);
        GameManager.Instance.OpenUI();
        if(OnCraftingUIOpened != null )
            OnCraftingUIOpened.Invoke();
    }

}
