using System;
using UnityEngine;
using static Enums;

public static class Structs
{
    [Serializable]
    public struct ItemQuantity
    {
        public string Name;
        public ItemList Item;
        public int Quantity;
    }
    
    [Serializable]
    public struct TimeDuration
    {
        [Range(0, 30)] public int Days;
        [Range(0, 23)] public int Hours;
        [Range(0, 59)] public int Minutes;
        [Range(0, 59)] public int Seconds;
    }
    
    [Serializable]
    public struct SkillLevel
    {
        public SkillList Skill;
        [Range(0, 6)] public int Level;
    }

    [Serializable]
    public struct ItemGather
    {
        public ItemList Item;
        [Tooltip("Can go down to 0.001")] [Range(0f, 1f)] public float BasePickupChance;
        public int BaseMinQuantity;
        public int BaseMaxQuantity;
    }
    
    [Serializable]
    public struct ActionGather
    {
        public string Name;
        public ItemGather[] RegionItems;
        public float TimeBase;
        public float TimeRemaining;
    }
    
    [Serializable]
    public struct StructureBuilt
    {
        public StructureList Structure;
        public bool Built;
    }
    
    
    [Serializable]
    public struct ToolCrafted
    {
        public ToolList Tool;
        public bool Crafted;
    }
}
