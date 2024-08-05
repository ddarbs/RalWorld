using UnityEngine;
using static Enums;
using static Structs;

/*
 * pickup idle thing will let you choose different regions to idle pickup
 * region will hold an array of grounditempickups and each tick rolls through them one by one to see if it passes it's own roll (could roll success on 10+ pickups if the array is 10 big)
 * 
 */

[CreateAssetMenu(menuName = "Data/Region Gather"), System.Serializable]
public class RegionGather : ScriptableObject
{
    public string Name;
    [Space(10f)]
    public SkillLevel Requirement;
    [Space(20f)]
    public ItemGather[] RegionItems;
    [Space(20f)]
    public TimeDuration Duration;
    
    public int GetTotalDuration()
    {
        return Duration.Seconds + (Duration.Minutes * 60) + (Duration.Hours * 3600) + (Duration.Days * 86400);
    }
}