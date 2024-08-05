using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ToolBenchTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] ToolBenchItemTexts = new TextMeshProUGUI[7];
    [SerializeField] private TextMeshProUGUI ToolBenchRequirementText;
    [SerializeField] private TextMeshProUGUI ToolBenchDurationText;
    [SerializeField] private TextMeshProUGUI ToolBenchOutputText;
    [SerializeField] private RectTransform TooltipBackground;
    [SerializeField] private GameObject ToolBenchTooltipObject;
    [SerializeField] private RectTransform RecipeTooltipTransform;
    [SerializeField] private RectTransform TooltipCanvas;
    [SerializeField] private Camera MainCamera;
    
    private const int BaseHeight = 250;
    private const int ItemHeight = 50;
    
    private static ToolBenchTooltip Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] should not be more than one ToolBenchTooltip" , this);
        }
    }

    private void Update()
    {
        if (!ToolBenchTooltipObject.activeSelf)
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
    
    public static void RequestTooltip(ToolRecipe _tool)
    {
        Instance.TooltipBackground.sizeDelta = new Vector2(Instance.TooltipBackground.sizeDelta.x, BaseHeight + (ItemHeight * _tool.ItemInputs.Length));
        for (int i = Instance.ToolBenchItemTexts.Length - 1; i >= _tool.ItemInputs.Length; i--)
        {
            Instance.ToolBenchItemTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _tool.ItemInputs.Length; i++)
        {
            if (i >= Instance.ToolBenchItemTexts.Length)
            {
                Debug.LogError("[ERROR] not enough texts to show all of the item inputs on tooltip", Instance);
                break;
            }

            Instance.ToolBenchItemTexts[i].text = $"{_tool.ItemInputs[i].Item} x{_tool.ItemInputs[i].Quantity}";
            Instance.ToolBenchItemTexts[i].transform.parent.gameObject.SetActive(true);
        }

        Instance.ToolBenchRequirementText.text = $"requires {_tool.Requirement.Skill} {_tool.Requirement.Level}";
        
        Instance.ToolBenchDurationText.text = $"{_tool.GetTotalDuration()}s";

        Instance.ToolBenchOutputText.text = $"unlocks {_tool.ToolLevel.Skill} {_tool.ToolLevel.Level}";
        
        Instance.ToolBenchTooltipObject.SetActive(true);
    }

    public static void EndTooltip()
    {
        Instance.ToolBenchTooltipObject.SetActive(false);
    }
}
