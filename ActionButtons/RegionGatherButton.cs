using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static Enums;

public class RegionGatherButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private RegionGather Region;
    [SerializeField] private Button Button;

    private void Awake()
    {
        PlayerInventoryVisuals.OnSkillUpdate += OnSkillUpdate;
    }

    public void Button_OnClick()
    {
        PlayerActionSystem.RequestAction(Region);
    }

    private void OnSkillUpdate(SkillList _skill, int _level)
    {
        if (_skill != Region.Requirement.Skill)
        {
            return;
        }

        if (_level < Region.Requirement.Level)
        {
            return;
        }
        
        Button.interactable = true;
        PlayerInventoryVisuals.OnSkillUpdate -= OnSkillUpdate;
        GenericActionAudio.RequestButtonInteractableAudio();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        GatherTooltip.RequestTooltip(Region);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        GatherTooltip.EndTooltip();
    }
}
