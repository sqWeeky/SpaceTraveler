using UnityEngine;

namespace SpaceTraveler.Scripts.UI.MapLevels
{
    public class DragMap : MonoBehaviour
    {
        [Header("Drag Settings")]
        public float dragSpeed = 1f;
    
        [Header("Map Settings")]
        public RectTransform mapRectTransform;
        public RectTransform boundaryRectTransform; // Границы (может быть родительский объект)
    
        private Vector2 dragOrigin;
        private bool isDragging;
        private Vector2 mapStartPosition;
        private Vector2 mapMinPosition;
        private Vector2 mapMaxPosition;

        void Start()
        {
            if (mapRectTransform == null)
                mapRectTransform = GetComponent<RectTransform>();
            
            CalculateBounds();
            mapStartPosition = mapRectTransform.anchoredPosition;
        }

        void Update()
        {
            HandleDrag();
        }

        void CalculateBounds()
        {
            if (boundaryRectTransform == null) return;

            // Вычисляем допустимые границы перемещения карты
            Vector2 boundarySize = boundaryRectTransform.rect.size;
            Vector2 mapSize = mapRectTransform.rect.size;
        
            // Максимальное смещение = разница размеров карты и границ
            Vector2 maxOffset = (mapSize - boundarySize) * 0.5f;
        
            mapMinPosition = mapStartPosition - maxOffset;
            mapMaxPosition = mapStartPosition + maxOffset;
        }

        void HandleDrag()
        {
            if (Input.GetMouseButtonDown(0))
            {
                dragOrigin = Input.mousePosition;
                isDragging = true;
            }

            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }

            if (isDragging && Input.GetMouseButton(0))
            {
                Vector2 currentMousePos = Input.mousePosition;
                Vector2 difference = (currentMousePos - dragOrigin) * dragSpeed;
            
                // Перемещаем карту в противоположном направлении
                Vector2 newPosition = mapRectTransform.anchoredPosition - difference;
            
                // Ограничиваем позицию
                newPosition.x = Mathf.Clamp(newPosition.x, mapMinPosition.x, mapMaxPosition.x);
                newPosition.y = Mathf.Clamp(newPosition.y, mapMinPosition.y, mapMaxPosition.y);
            
                mapRectTransform.anchoredPosition = newPosition;
            
                dragOrigin = currentMousePos;
            }
        }
    }
}
