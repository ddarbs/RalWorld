using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BuildTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] BuildItemTexts = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI BuildRequirementText;
    [SerializeField] private TextMeshProUGUI BuildDurationText;
    [SerializeField] private TextMeshProUGUI BuildOutputText;
    [SerializeField] private RectTransform TooltipBackground;
    [SerializeField] private GameObject BuildTooltipObject;
    [SerializeField] private RectTransform RecipeTooltipTransform;
    [SerializeField] private RectTransform TooltipCanvas;
    [SerializeField] private Camera MainCamera;
    
    private const int BaseHeight = 250;
    private const int ItemHeight = 50;
    
    private static BuildTooltip Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] should not be more than one BuildTooltip" , this);
        }
    }
    
    private void Update()
    {
        if (!BuildTooltipObject.activeSelf)
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

    public static void RequestTooltip(StructureRecipe _structure)
    {
        Instance.TooltipBackground.sizeDelta = new Vector2(Instance.TooltipBackground.sizeDelta.x, BaseHeight + (ItemHeight * _structure.ItemInputs.Length));
        for (int i = Instance.BuildItemTexts.Length - 1; i >= _structure.ItemInputs.Length; i--)
        {
            Instance.BuildItemTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _structure.ItemInputs.Length; i++)
        {
            if (i >= Instance.BuildItemTexts.Length)
            {
                Debug.LogError("[ERROR] not enough texts to show all of the item inputs on tooltip", Instance);
                break;
            }

            Instance.BuildItemTexts[i].text = $"{_structure.ItemInputs[i].Item} x{_structure.ItemInputs[i].Quantity}";
            Instance.BuildItemTexts[i].transform.parent.gameObject.SetActive(true);
        }

        Instance.BuildRequirementText.text = $"requires {_structure.Requirement.Skill} {_structure.Requirement.Level}";
        
        Instance.BuildDurationText.text = $"{_structure.GetTotalDuration()}s";

        Instance.BuildOutputText.text = $"unlocks {_structure.StructureLevel.Skill} {_structure.StructureLevel.Level}";
        
        Instance.BuildTooltipObject.SetActive(true);
    }

    public static void EndTooltip()
    {
        Instance.BuildTooltipObject.SetActive(false);
    }
}
