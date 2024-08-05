using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class PlayerActionSystem : MonoBehaviour
{
    // TODO: player can gather, craft, and fight
    // ral's have their own automation system

    [SerializeField] private TextMeshProUGUI ProgressDescription;
    [SerializeField] private Slider ProgressVisual;
    [SerializeField] private ItemInventory ItemInventory;
    [SerializeField] private SkillInventory SkillInventory;
    [SerializeField] private StructureInventory StructureInventory;
    [SerializeField] private ToolInventory ToolInventory;
    [SerializeField] private GameObject StopActionButton;
    
    private static PlayerActionSystem Instance;

    private static Coroutine ActiveAction;
    private static Coroutine ActiveProgress;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("Should not be two PlayerActionSystem", this);
        }
    }
    
    public static void Stop()
    {
        if (ActiveAction == null)
        {
            return;
        }
        
        Instance.StopCoroutine(ActiveAction);
        Instance.StopCoroutine(ActiveProgress);
        
        Instance.ProgressVisual.value = 0f;
        Instance.ProgressDescription.text = "browsing Alteryx dashboards";
        Instance.StopActionButton.SetActive(false);
    }
    
#region Item    
    public static void RequestAction(ItemRecipe _recipe)
    {
        if (ActiveAction != null)
        {
            Instance.StopCoroutine(ActiveAction);
            Instance.StopCoroutine(ActiveProgress);
        }
        ActiveAction = Instance.StartCoroutine(RecipeThread(_recipe));
        
        Instance.StopActionButton.SetActive(true);
    }
    private static IEnumerator RecipeThread(ItemRecipe _recipe)
    {
        Top:
        float _Duration = _recipe.GetTotalDuration();
        int _RequirementLevel = _recipe.Requirement.Level;
        int _PlayerLevel = Instance.SkillInventory.Skills[(int)_recipe.Requirement.Skill].Level;
        if (_RequirementLevel < _PlayerLevel)
        {
            _Duration /= (1 + _PlayerLevel - _RequirementLevel);
        }
        ActiveProgress = Instance.StartCoroutine(VisualProgressThread(_Duration, "Refining " + _recipe.Name));
        
        yield return new WaitForSeconds(_Duration);
        foreach (var _Input in _recipe.ItemInputs)
        {
            Instance.ItemInventory.RemoveQuantity(_Input.Item, _Input.Quantity);
        }
        foreach (var _Output in _recipe.ItemOutputs)
        {
            Instance.ItemInventory.AddQuantity(_Output.Item, _Output.Quantity);
        }

        foreach (var _Input in _recipe.ItemInputs)
        {
            if (Instance.ItemInventory.CheckQuantity(_Input.Item) < _Input.Quantity)
            {
                Instance.ProgressVisual.value = 0f;
                Instance.ProgressDescription.text = "browsing Alteryx dashboards";
                Instance.StopActionButton.SetActive(false);
                yield break;
            }
        }
        
        GenericActionAudio.RequestForgeAudio();
        
        yield return new WaitForSeconds(1f);
        goto Top;
        //ActiveAction = Instance.StartCoroutine(RecipeThread(_recipe));
    }
#endregion Item

#region Gather
    public static void RequestAction(RegionGather _gather)
    {
        if (ActiveAction != null)
        {
            Instance.StopCoroutine(ActiveAction);
            Instance.StopCoroutine(ActiveProgress);
        }
        ActiveAction = Instance.StartCoroutine(GatherThread(_gather));
        
        Instance.StopActionButton.SetActive(true);
    }
    private static IEnumerator GatherThread(RegionGather _gather)
    {
        Top:
        float _Duration = _gather.GetTotalDuration();
        int _RequirementLevel = _gather.Requirement.Level;
        int _PlayerLevel = Instance.SkillInventory.Skills[(int)_gather.Requirement.Skill].Level;
        if (_RequirementLevel < _PlayerLevel)
        {
            _Duration /= (1 + _PlayerLevel - _RequirementLevel);
        }
        ActiveProgress = Instance.StartCoroutine(VisualProgressThread(_Duration, "Gathering in " + _gather.Name));
        
        yield return new WaitForSeconds(_Duration);

        foreach (var _Region in _gather.RegionItems)
        {
            int _Roll = Random.Range(1, 1001);
            Debug.Log($"item is {_Region.Item}, max is {_Region.BasePickupChance * 1000f} and rolled {_Roll}", Instance);
            if (_Roll <= _Region.BasePickupChance * 1000f)
            {
                int _Quantity = Random.Range(_Region.BaseMinQuantity, _Region.BaseMaxQuantity + 1);
                Instance.ItemInventory.AddQuantity(_Region.Item, _Quantity);
            }
        }

        GenericActionAudio.RequestGatherAudio();
        
        yield return new WaitForSeconds(1f);
        goto Top;
        //ActiveAction = Instance.StartCoroutine(GatherThread(_gather));
    }
#endregion Gather

#region Structure    
    public static void RequestAction(StructureRecipe _recipe)
    {
        if (ActiveAction != null)
        {
            Instance.StopCoroutine(ActiveAction);
            Instance.StopCoroutine(ActiveProgress);
        }
        ActiveAction = Instance.StartCoroutine(RecipeThread(_recipe));
        
        Instance.StopActionButton.SetActive(true);
    }
    
    private static IEnumerator RecipeThread(StructureRecipe _recipe)
    {
        float _Duration = _recipe.GetTotalDuration();
        int _RequirementLevel = _recipe.Requirement.Level;
        int _PlayerLevel = Instance.SkillInventory.Skills[(int)_recipe.Requirement.Skill].Level;
        if (_RequirementLevel < _PlayerLevel)
        {
            _Duration /= (1 + _PlayerLevel - _RequirementLevel);
        }
        ActiveProgress = Instance.StartCoroutine(VisualProgressThread(_Duration, "Building " + _recipe.Name));
        
        yield return new WaitForSeconds(_Duration);
        foreach (var _Input in _recipe.ItemInputs)
        {
            Instance.ItemInventory.RemoveQuantity(_Input.Item, _Input.Quantity);
        }
        
        Instance.StructureInventory.BuildStructure(_recipe.StructureOutput);
        while (Instance.SkillInventory.Skills[(int)_recipe.StructureLevel.Skill].Level < _recipe.StructureLevel.Level)
        {
            Instance.SkillInventory.IncreaseLevel(_recipe.StructureLevel.Skill);
        }
        
        Instance.ProgressVisual.value = 0f;
        Instance.ProgressDescription.text = "browsing Alteryx dashboards";
        Instance.StopActionButton.SetActive(false);
        
        GenericActionAudio.RequestBuildAudio();
    }
#endregion Structure

#region Tool
    public static void RequestAction(ToolRecipe _recipe)
    {
        if (ActiveAction != null)
        {
            Instance.StopCoroutine(ActiveAction);
            Instance.StopCoroutine(ActiveProgress);
        }
        ActiveAction = Instance.StartCoroutine(RecipeThread(_recipe));
        
        Instance.StopActionButton.SetActive(true);
    }
    
    private static IEnumerator RecipeThread(ToolRecipe _recipe)
    {
        float _Duration = _recipe.GetTotalDuration();
        int _RequirementLevel = _recipe.Requirement.Level;
        int _PlayerLevel = Instance.SkillInventory.Skills[(int)_recipe.Requirement.Skill].Level;
        if (_RequirementLevel < _PlayerLevel)
        {
            _Duration /= (1 + _PlayerLevel - _RequirementLevel);
        }
        ActiveProgress = Instance.StartCoroutine(VisualProgressThread(_Duration, "Crafting " + _recipe.Name));
        
        yield return new WaitForSeconds(_Duration);
        foreach (var _Input in _recipe.ItemInputs)
        {
            Instance.ItemInventory.RemoveQuantity(_Input.Item, _Input.Quantity);
        }
        
        Instance.ToolInventory.CraftTool(_recipe.ToolOutput);
        while (Instance.SkillInventory.Skills[(int)_recipe.ToolLevel.Skill].Level < _recipe.ToolLevel.Level)
        {
            Instance.SkillInventory.IncreaseLevel(_recipe.ToolLevel.Skill);
        }
        
        Instance.ProgressVisual.value = 0f;
        Instance.ProgressDescription.text = "browsing Alteryx dashboards";
        Instance.StopActionButton.SetActive(false);
        
        GenericActionAudio.RequestToolBenchAudio();
    }

    #endregion Tools
    
    private static IEnumerator VisualProgressThread(float _time, string _description)
    {
        Instance.ProgressVisual.value = 0f;
        Instance.ProgressDescription.text = _description;
        
        float _Remaining = _time;
        while (_Remaining > 0f)
        {
            yield return new WaitForSeconds(0.25f);
            _Remaining -= 0.25f;
            Instance.ProgressVisual.value = 1f - (_Remaining / _time);
        }
        
        Instance.ProgressVisual.value = 0f;
    }
}
