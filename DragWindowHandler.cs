using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragWindowHandler : MonoBehaviour, IDragHandler
{
    private RectTransform ParentTransform;
    private Canvas ParentCanvas;

    private bool CanDrag = false;

    private void Awake()
    {
        ParentTransform = transform.parent.parent.GetComponent<RectTransform>();
        ParentCanvas = GetComponentInParent<Canvas>();

        IntroTutorial.OnIntroEnd += OnIntroEnd;
    }

    public void OnDrag(PointerEventData _eventData)
    {
        if (!CanDrag)
        {
            return;
        }
        
        ParentTransform.anchoredPosition += _eventData.delta / ParentCanvas.scaleFactor;
    }

    private void OnIntroEnd()
    {
        CanDrag = true;
        IntroTutorial.OnIntroEnd -= OnIntroEnd;
    }
}
