using System;
using UnityEngine;
using static Structs;
using static Enums;

/*
 * players can increase their profession skills by crafting tools, up to 4 out of a possible 5 that ral's can
 * 
 */

[CreateAssetMenu(menuName = "Data/Skill Inventory"), System.Serializable]
public class SkillInventory : ScriptableObject
{
    public SkillLevel[] Skills = new SkillLevel[Enum.GetNames(typeof(SkillList)).Length];

    public void SetupSkills() // DEBUG
    {
        for (int i = 0; i < Skills.Length; i++)
        {
            //Skills[i].Name = ((SkillList)i).ToString();
            Skills[i].Skill = (SkillList)i;
        }
    }

    public void IncreaseLevel(SkillList _skill)
    {
        Skills[(int)_skill].Level++;
        
        PlayerInventoryVisuals.UpdateSkillTexts(_skill);
    }
}
