using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GatherTooltip : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI[] GatherItemTexts = new TextMeshProUGUI[5];
    [SerializeField] private TextMeshProUGUI GatherRequirementText;
    [SerializeField] private TextMeshProUGUI GatherDurationText;
    [SerializeField] private RectTransform TooltipBackground;
    [SerializeField] private GameObject GatherTooltipObject;
    
    [SerializeField] private RectTransform RecipeTooltipTransform;
    [SerializeField] private RectTransform TooltipCanvas;
    [SerializeField] private Camera MainCamera;
    
    private const int GatherBaseHeight = 200;
    private const int GatherItemHeight = 50;
    
    private static GatherTooltip Instance;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Debug.LogError("[ERROR] should not be more than one GatherTooltip" , this);
        }
    }
    
    private void Update()
    {
        if (!GatherTooltipObject.activeSelf)
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

    public static void RequestTooltip(RegionGather _region)
    {
        Instance.TooltipBackground.sizeDelta = new Vector2(Instance.TooltipBackground.sizeDelta.x, GatherBaseHeight + (GatherItemHeight * _region.RegionItems.Length));
        for (int i = Instance.GatherItemTexts.Length - 1; i >= _region.RegionItems.Length; i--)
        {
            Instance.GatherItemTexts[i].transform.parent.gameObject.SetActive(false);
        }
        
        for (int i = 0; i < _region.RegionItems.Length; i++)
        {
            if (i >= Instance.GatherItemTexts.Length)
            {
                Debug.LogError("[ERROR] not enough texts to show all of the gatherables on tooltip", Instance);
                break;
            }

            Instance.GatherItemTexts[i].text = $"{_region.RegionItems[i].Item} | {(_region.RegionItems[i].BasePickupChance * 100f):N0}% | {_region.RegionItems[i].BaseMinQuantity} - {_region.RegionItems[i].BaseMaxQuantity}";
            Instance.GatherItemTexts[i].transform.parent.gameObject.SetActive(true);
        }

        Instance.GatherRequirementText.text = $"requires {_region.Requirement.Skill} {_region.Requirement.Level}";
        
        Instance.GatherDurationText.text = $"{_region.GetTotalDuration()}s";
        
        Instance.GatherTooltipObject.SetActive(true);
    }

    public static void EndTooltip()
    {
        Instance.GatherTooltipObject.SetActive(false);
    }
}
