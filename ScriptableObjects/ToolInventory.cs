using System;
using UnityEngine;
using UnityEngine.Serialization;
using static Structs;
using static Enums;

/*
 * players can increase their profession skills by crafting tools, up to 4 out of a possible 5 that ral's can
 * 
 */

[CreateAssetMenu(menuName = "Data/Tool Inventory"), System.Serializable]
public class ToolInventory : ScriptableObject
{
    public ToolCrafted[] Tools = new ToolCrafted[Enum.GetNames(typeof(ToolList)).Length];

    public void SetupSkills() // DEBUG
    {
        for (int i = 0; i < Tools.Length; i++)
        {
            Tools[i].Tool = (ToolList)i;
        }
    }

    public void CraftTool(ToolList _tool)
    {
        Tools[(int)_tool].Crafted = true;
        
        PlayerInventoryVisuals.UpdateToolTexts(_tool);
    }
}
