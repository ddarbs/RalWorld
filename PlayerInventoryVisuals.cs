using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using static Enums;
using static Structs;

public class PlayerInventoryVisuals : MonoBehaviour
{
    private static PlayerInventoryVisuals Instance;

    public delegate void ItemUpdate(ItemList _item, int _quantity);
    public static event ItemUpdate OnItemUpdate;
    
    public delegate void SkillUpdate(SkillList _skill, int _level);
    public static event SkillUpdate OnSkillUpdate;
    
    public delegate void StructureUpdate(StructureList _structure);
    public static event StructureUpdate OnStructureUpdate;
    
    public delegate void ToolUpdate(ToolList _tool);
    public static event ToolUpdate OnToolUpdate;
    
    [FormerlySerializedAs("Inventory")] [SerializeField] private ItemInventory ItemInventory;
    [FormerlySerializedAs("Texts")] [SerializeField] private TextMeshProUGUI[] MaterialTexts = new TextMeshProUGUI[Enum.GetNames(typeof(ItemList)).Length];
    
    [SerializeField] private SkillInventory SkillInventory;
    [SerializeField] private TextMeshProUGUI[] SkillTexts = new TextMeshProUGUI[Enum.GetNames(typeof(SkillList)).Length];

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Should not be two PlayerInventoryVisuals", this);
        }
    }

    public static void UpdateMaterialTexts(ItemList _item)
    {
        int _Quantity = Instance.ItemInventory.Slots[(int)_item].Quantity;
        Instance.MaterialTexts[(int)_item].text = _Quantity.ToString("N0");
        
        //Debug.Log($"item is {(int)_item}_{_item}, setting text {Instance.MaterialTexts[(int)_item].name} to {Instance.ItemInventory.Slots[(int)_item].Quantity}", Instance);
        
        OnItemUpdate?.Invoke(_item, _Quantity);
    }
    
    public static void UpdateSkillTexts(SkillList _skill)
    {
        int _Level = Instance.SkillInventory.Skills[(int)_skill].Level;
        Instance.SkillTexts[(int)_skill].text = _Level.ToString();
        
        //Debug.Log($"skill is {(int)_skill}_{_skill}, setting text {Instance.SkillTexts[(int)_skill].name} to {Instance.SkillInventory.Skills[(int)_skill].Level}", Instance);
        
        OnSkillUpdate?.Invoke(_skill, _Level);
    }
    
    public static void UpdateStructureTexts(StructureList _structure)
    {
        OnStructureUpdate?.Invoke(_structure);
    }
    
    public static void UpdateToolTexts(ToolList _tool)
    {
        OnToolUpdate?.Invoke(_tool);
    }
}
