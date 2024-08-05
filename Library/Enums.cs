using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Enums
{
    // TODO: item list will contain every single item/tool/armor/non-pal-obtainable in the game,
    //       but will only show certain things in certain places, like a materials inventory ui, 
    //       tools inventory ui, combat inventory ui, etc
    public enum ItemList
    {
        Stone = 0,
        Wood = 1,
        RalCrystal = 2,
        WoodFiber = 3,
        ImpureOre = 4,
        ImpureIngot = 5,
        RalPart = 6,
        
        
        // TODO: later
        //Charcoal = 4,
        //Brick = 5,
        //PureOre = 6,
    }

    public enum ItemFilter
    {
        Material = 0,
        Tool = 1,
        Weapon = 2,
        Armor = 3,
        Sphere = 4,
    }

    /*
     * changes:
     *      Gathering is something new
     *      Farming + Planting = Farming
     *      Combat = fighting/catching Rals
     */
    public enum SkillList
    {
        Combat = 0,
        Cooling = 1,
        Electricity = 2,
        Farming = 3,
        Gathering = 4,
        Handiwork = 5,
        Kindling = 6,
        Lumbering = 7,
        Medicine = 8,
        Mining = 9,
        Watering = 10,
    }
    
    public enum StructureList
    {
        Forge = 0,
        ToolBench = 1,
        Armory = 2,
        Weaponsmith = 3,
    }
    
    
    public enum ToolList
    {
        CrudeAxe = 0,
        CrudePick = 1,
        Shovel = 2,
        MetalAxe = 3,
        MetalPick = 4,
    }
}
