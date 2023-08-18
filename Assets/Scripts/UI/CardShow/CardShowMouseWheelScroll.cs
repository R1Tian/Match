using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardShowMouseWheelScroll : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    private static ScrollRect scrollRect;
    private RectTransform rectTransform;
    public float scrollSpeed = 100.0f;

    private bool isPointerOverViewport = false;
    

    private void Awake()
    {
        scrollRect = GetComponent<ScrollRect>();
        //Debug.Log(scrollRect.name);
        rectTransform = scrollRect.content;
    }

    private void Start()
    {
        rectTransform.sizeDelta = new Vector2(0, ShowCardInBattle.Height);
        UpdateViewport();
    }

    public static void UpdateViewport()
    {
        Canvas.ForceUpdateCanvases(); // 强制更新画布以确保大小生效
        scrollRect.verticalNormalizedPosition = 1;
    }

    private void Update()
    {
        if (isPointerOverViewport)
        {
            float scrollDelta = Input.GetAxis("Mouse ScrollWheel");
            Debug.Log("Scroll Delta: " + scrollDelta);
            scrollRect.verticalNormalizedPosition += scrollDelta * scrollSpeed;
            scrollRect.verticalNormalizedPosition = Mathf.Clamp01(scrollRect.verticalNormalizedPosition);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        isPointerOverViewport = true;
        // rectTransform.sizeDelta = new Vector2(0, ShowCardInBattle.Height);
        // Canvas.ForceUpdateCanvases(); // 强制更新画布以确保大小生效
        // scrollRect.verticalNormalizedPosition = 1; 
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        isPointerOverViewport = false;
    }
}
