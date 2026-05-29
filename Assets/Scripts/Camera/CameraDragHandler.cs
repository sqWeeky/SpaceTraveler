using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CameraDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
{
    [SerializeField] private RectTransform mapContainer; // контейнер с фоном и всеми кнопками
    [SerializeField] private float dragSpeed = 1f;
    [SerializeField] private Vector2 minPosition;
    [SerializeField] private Vector2 maxPosition;

    private Vector2 lastMousePosition;

    public void OnBeginDrag(PointerEventData eventData)
    {
        lastMousePosition = eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 delta = eventData.position - lastMousePosition;
        Vector2 newPos = mapContainer.anchoredPosition + delta * dragSpeed;
        
        // Ограничиваем перемещение, чтобы не уйти за края фона
        newPos.x = Mathf.Clamp(newPos.x, minPosition.x, maxPosition.x);
        newPos.y = Mathf.Clamp(newPos.y, minPosition.y, maxPosition.y);
        
        mapContainer.anchoredPosition = newPos;
        lastMousePosition = eventData.position;
    }
}
