using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using static Structs;
using static Enums;

/*
 * Recipe Time = Duration / 1 + Ral Skill - Recipe Skill (up to 1/5 time for a t5 ral on a t1 craft)
 * player can only do recipe if their skills are at least the requirement level
 * 
 */


[CreateAssetMenu(menuName = "Data/Item Recipe"), System.Serializable]
public class ItemRecipe : ScriptableObject
{
    public string Name;
    [Header("Requirements")] 
    public SkillLevel Requirement;
    public ItemQuantity[] ItemInputs;
    public TimeDuration Duration;
    [Header("Gain")]
    public ItemQuantity[] ItemOutputs;
    [Space(10f)]
    [Header("Offline")] 
    public bool Started;
    public float Remaining;

    public int GetTotalDuration()
    {
        return Duration.Seconds + (Duration.Minutes * 60) + (Duration.Hours * 3600) + (Duration.Days * 86400);
    }
}
