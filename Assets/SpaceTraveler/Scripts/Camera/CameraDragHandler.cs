using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace SpaceTraveler.Scripts.Camera
{
    public class CameraDragHandler : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        [Header("Drag Settings")]
        [SerializeField] private RectTransform mapContainer;
        [SerializeField] private float dragSpeed = 1f;
        [SerializeField] private Vector2 minPosition;
        [SerializeField] private Vector2 maxPosition;

        [Header("Auto Center on Enable")]
        [SerializeField] private bool centerOnEnable = true;
        [SerializeField] private List<Transform> _levelButtons;

        [Space]
        [SerializeField] private bool _initialized;

        private Vector2 lastMousePosition;

        public bool Initialized => _initialized;

        public void Init(List<Transform> levelButtons)
        {
            _initialized = true;

            if (_levelButtons.Count == 0)
            {
                foreach (var levelButton in levelButtons) 
                    _levelButtons.Add(levelButton);
            }
            
            if (centerOnEnable)
            {
                CenterOnFirstLockedOrFirstLevel();
            }
        }

        private void OnEnable()
        {
            if (centerOnEnable)
            {
                CenterOnFirstLockedOrFirstLevel();
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            lastMousePosition = eventData.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            Vector2 delta = eventData.position - lastMousePosition;
            Vector2 newPos = mapContainer.anchoredPosition + delta * dragSpeed;

            newPos.x = Mathf.Clamp(newPos.x, minPosition.x, maxPosition.x);
            newPos.y = Mathf.Clamp(newPos.y, minPosition.y, maxPosition.y);

            mapContainer.anchoredPosition = newPos;
            lastMousePosition = eventData.position;
        }

        private void CenterOnFirstLockedOrFirstLevel()
        {
            if (_levelButtons == null || _levelButtons.Count == 0) return;

            Transform targetButton = GetFirstLockedOrFirstButton();

            if (targetButton != null)
            {
                RectTransform buttonRect = targetButton.GetComponent<RectTransform>();
                if (buttonRect != null)
                {
                    Vector2 targetPos = -buttonRect.anchoredPosition;
                    targetPos.x = Mathf.Clamp(targetPos.x, minPosition.x, maxPosition.x);
                    targetPos.y = Mathf.Clamp(targetPos.y, minPosition.y, maxPosition.y);
                    mapContainer.anchoredPosition = targetPos;
                }
            }
        }

        private Transform GetFirstLockedOrFirstButton()
        {
            foreach (Transform button in _levelButtons)
            {
                Button btn = button.GetComponent<Button>();
                if (btn != null && !btn.interactable)
                    return button;

                //LevelButtonData levelData = button.GetComponent<LevelButtonData>();
                // if (levelData != null && levelData.isLocked)
                //     return button;
            }

            return _levelButtons.Count > 0 ? _levelButtons[0] : null;
        }
    }
}