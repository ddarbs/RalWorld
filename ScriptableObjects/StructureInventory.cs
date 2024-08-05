using System;
using UnityEngine;
using UnityEngine.Serialization;
using static Structs;
using static Enums;

/*
 * players can increase their profession skills by crafting tools, up to 4 out of a possible 5 that ral's can
 * 
 */

[CreateAssetMenu(menuName = "Data/Structure Inventory"), System.Serializable]
public class StructureInventory : ScriptableObject
{
    public StructureBuilt[] Structures = new StructureBuilt[Enum.GetNames(typeof(StructureList)).Length];

    public void SetupSkills() // DEBUG
    {
        for (int i = 0; i < Structures.Length; i++)
        {
            Structures[i].Structure = (StructureList)i;
        }
    }

    public void BuildStructure(StructureList _structure)
    {
        Structures[(int)_structure].Built = true;
        
        PlayerInventoryVisuals.UpdateStructureTexts(_structure);
    }
}
