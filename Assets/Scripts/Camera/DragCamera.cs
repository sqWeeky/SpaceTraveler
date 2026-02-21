using UnityEngine;

namespace Camera
{
    public class DragCamera : MonoBehaviour
    {
        [Header("Camera Drag Settings")]
        public SpriteRenderer background; // Перетащите сюда ваш объект заднего фона

        private Vector3 dragOrigin;
        private bool isDragging = false;

        [SerializeField] private UnityEngine.Camera mainCamera;
        private Bounds backgroundBounds;

        void Start()
        {
            if (background != null)
            {
                backgroundBounds = background.bounds;
            }
            else
            {
                Debug.LogError("Background not assigned!");
            }
        }

        void Update()
        {
            HandleMouseInput();
        }

        void HandleMouseInput()
        {
            // Начало перетаскивания
            if (Input.GetMouseButtonDown(0))
            {
                StartDragging();
            }

            // Процесс перетаскивания
            if (Input.GetMouseButton(0) && isDragging)
            {
                Drag();
            }

            // Конец перетаскивания
            if (Input.GetMouseButtonUp(0))
            {
                isDragging = false;
            }
        }

        void StartDragging()
        {
            isDragging = true;
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        void Drag()
        {
            Vector3 currentPos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            Vector3 difference = dragOrigin - currentPos;

            // Перемещаем камеру
            transform.position += difference;

            // Ограничиваем позицию камеры границами фона
            ClampCameraToBackground();

            // Обновляем точку начала перетаскивания для плавного движения
            dragOrigin = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        }

        void ClampCameraToBackground()
        {
            if (background == null) return;

            // Получаем размеры камеры в мировых координатах
            float cameraHeight = 2f * mainCamera.orthographicSize;
            float cameraWidth = cameraHeight * mainCamera.aspect;

            // Вычисляем границы, в которых может находиться камера
            float minX = backgroundBounds.min.x + cameraWidth / 2f;
            float maxX = backgroundBounds.max.x - cameraWidth / 2f;
            float minY = backgroundBounds.min.y + cameraHeight / 2f;
            float maxY = backgroundBounds.max.y - cameraHeight / 2f;

            // Ограничиваем позицию камеры
            float clampedX = Mathf.Clamp(transform.position.x, minX, maxX);
            float clampedY = Mathf.Clamp(transform.position.y, minY, maxY);

            transform.position = new Vector3(clampedX, clampedY, transform.position.z);
        }
    }
}