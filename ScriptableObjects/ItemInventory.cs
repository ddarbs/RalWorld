using System;
using TMPro;
using UnityEngine;
using static Structs;
using static Enums;

[CreateAssetMenu(menuName = "Data/Item Inventory"), System.Serializable]
public class ItemInventory : ScriptableObject
{
    public ItemQuantity[] Slots = new ItemQuantity[Enum.GetNames(typeof(ItemList)).Length];


    public void SetupSlots() // DEBUG
    {
        for (int i = 0; i < Slots.Length; i++)
        {
            Slots[i].Name = ((ItemList)i).ToString();
            Slots[i].Item = (ItemList)i;
        }
    }

    public int CheckQuantity(ItemList _item)
    {
        return Slots[(int)_item].Quantity;
    }
    
    public void AddQuantity(ItemList _item, int _quantity)
    {
        Slots[(int)_item].Quantity += _quantity;
        // TODO: call event/action 

        PlayerInventoryVisuals.UpdateMaterialTexts(_item);
    }
    
    public void RemoveQuantity(ItemList _item, int _quantity)
    {
        Slots[(int)_item].Quantity -= _quantity;
        if (Slots[(int)_item].Quantity < 0)
        {
            Debug.LogError("[ERROR] somehow ItemInventory has a negative quantity value for " + _item, this);
        }
        // TODO: call event/action 

        PlayerInventoryVisuals.UpdateMaterialTexts(_item);
    }
}