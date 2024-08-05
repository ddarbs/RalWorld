using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ItemRecipeTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] RecipeItemInputTexts = new TextMeshProUGUI[7];
    [SerializeField] private TextMeshProUGUI[] RecipeItemOutputTexts = new TextMeshProUGUI[4];
    [SerializeField] private TextMeshProUGUI RecipeRequirementText;
    [SerializeField] private TextMeshProUGUI RecipeDurationText;
    [SerializeField] private RectTransform TooltipBackground;
    [SerializeField] private GameObject RecipeTooltipObject;
    [SerializeField] private RectTransform RecipeTooltipTransform;
    [SerializeField] private RectTransform TooltipCanvas;
    [SerializeField] private Camera MainCamera;
    
    private const int BaseHeight = 250;
    private const int ItemHeight = 50;
    
    private static ItemRecipeTooltip Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] should not be more than one ItemRecipeTooltip" , this);
        }
    }

    private void Update()
    {
        if (!RecipeTooltipObject.activeSelf)
        {
            return;
        }

        RectTransformUtility.ScreenPointToLocalPointInRectangle(TooltipCanvas, Input.mousePosition, MainCamera, out Vector2 _LocalPoint);
        RecipeTooltipTransform.localPosition = _LocalPoint;
        
        Vector2 TooltipPosition = RecipeTooltipTransform.anchoredPosition;

        if (TooltipPosition.x > TooltipCanvas.rect.width / 2f)
        {
            TooltipPosition.x -= 400f;
        }
        else
        {
            TooltipPosition.x += 400f;
        }
        
        if (TooltipPosition.y > TooltipCanvas.rect.height / 2f)
        {
            TooltipPosition.y -= 100f;
        }
        else
        {
            TooltipPosition.y += TooltipBackground.rect.height / 2f;
        }

        RecipeTooltipTransform.anchoredPosition = TooltipPosition;
    }

    public static void RequestTooltip(ItemRecipe _recipe)
    {
        Instance.TooltipBackground.sizeDelta = new Vector2(
            Instance.TooltipBackground.sizeDelta.x, 
            BaseHeight + (ItemHeight * _recipe.ItemInputs.Length) + (ItemHeight * _recipe.ItemOutputs.Length)
            );
        
        for (int i = Instance.RecipeItemInputTexts.Length - 1; i >= _recipe.ItemInputs.Length; i--)
        {
            Instance.RecipeItemInputTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _recipe.ItemInputs.Length; i++)
        {
            if (i >= Instance.RecipeItemInputTexts.Length)
            {
                Debug.LogError("[ERROR] not enough texts to show all of the item inputs on tooltip", Instance);
                break;
            }

            Instance.RecipeItemInputTexts[i].text = $"{_recipe.ItemInputs[i].Item} x{_recipe.ItemInputs[i].Quantity}";
            Instance.RecipeItemInputTexts[i].transform.parent.gameObject.SetActive(true);
        }

        Instance.RecipeRequirementText.text = $"requires {_recipe.Requirement.Skill} {_recipe.Requirement.Level}";
        
        Instance.RecipeDurationText.text = $"{_recipe.GetTotalDuration()}s";

        for (int i = Instance.RecipeItemOutputTexts.Length - 1; i >= _recipe.ItemOutputs.Length; i--)
        {
            Instance.RecipeItemOutputTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _recipe.ItemOutputs.Length; i++)
        {
            if (i >= Instance.RecipeItemOutputTexts.Length)
            {
                Debug.LogError("[ERROR] not enough texts to show all of the item inputs on tooltip", Instance);
                break;
            }

            Instance.RecipeItemOutputTexts[i].text = $"{_recipe.ItemOutputs[i].Item} x{_recipe.ItemOutputs[i].Quantity}";
            Instance.RecipeItemOutputTexts[i].transform.parent.gameObject.SetActive(true);
        }
        
        Instance.RecipeTooltipObject.SetActive(true);
    }

    public static void EndTooltip()
    {
        Instance.RecipeTooltipObject.SetActive(false);
    }
}
