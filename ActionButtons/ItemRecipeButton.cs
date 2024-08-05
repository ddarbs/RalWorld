using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Enums;

public class ItemRecipeButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private ItemRecipe Recipe;
    private Button Button;

    private Dictionary<ItemList, bool> Ingredients = new Dictionary<ItemList, bool>();

    private bool Leveled = false;
    
    private void Awake()
    {
        Button = GetComponent<Button>();
        
        PlayerInventoryVisuals.OnSkillUpdate += OnSkillUpdate;
        PlayerInventoryVisuals.OnItemUpdate += OnItemUpdate;
    }

    private void Start()
    {
        if (Recipe.Requirement.Level == 0)
        {
            Leveled = true;
        }
    }
    
    public void Button_OnClick()
    {
        PlayerActionSystem.RequestAction(Recipe);
    }

    private void OnItemUpdate(ItemList _item, int _quantity)
    {
        foreach (var _Input in Recipe.ItemInputs)
        {
            if (_item != _Input.Item)
            {
                continue;
            }

            bool HasIngredient = Ingredients.ContainsKey(_item);
            
            if (_quantity < _Input.Quantity)
            {
                if (HasIngredient)
                {
                    Ingredients.Remove(_item);
                }
                Button.interactable = false;
                return;
            }
            
            if (HasIngredient)
            {
                break;
            }
            Ingredients.Add(_item, true);
            break;
        }
        
        if (Ingredients.Count < Recipe.ItemInputs.Length)
        {
            Button.interactable = false;
            return;
        }

        if (!Leveled)
        {
            return;
        }
        
        Button.interactable = true;
        GenericActionAudio.RequestButtonInteractableAudio();
    }
    
    private void OnSkillUpdate(SkillList _skill, int _level)
    {
        if (_skill != Recipe.Requirement.Skill)
        {
            return;
        }

        if (_level < Recipe.Requirement.Level)
        {
            return;
        }
        
        Leveled = true;
        
        if (Ingredients.Count < Recipe.ItemInputs.Length)
        {
            return;
        }
        
        Button.interactable = true;
        PlayerInventoryVisuals.OnSkillUpdate -= OnSkillUpdate;
        GenericActionAudio.RequestButtonInteractableAudio();
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        ItemRecipeTooltip.RequestTooltip(Recipe);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        ItemRecipeTooltip.EndTooltip();
    }
}
