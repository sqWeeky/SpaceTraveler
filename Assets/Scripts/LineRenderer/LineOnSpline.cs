using NaughtyAttributes;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Splines;

namespace LineRenderer
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class SplineToLineRenderer : MonoBehaviour
    {
        [SerializeField] private SplineContainer _splineContainer;
        [SerializeField] private int _resolution = 50; // Количество сегментов

        private UnityEngine.LineRenderer _lineRenderer;

        private void Awake()
        {
            _lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            _lineRenderer.startWidth = 1.0f;  // Толстая в начале
            _lineRenderer.endWidth = 1.0f; 
            DrawLine();
        }

        private void DrawLine()
        {
            if (_splineContainer == null) return;
        
            Spline spline = _splineContainer.Spline;
        
            // Получаем все узлы сплайна в локальных координатах
            Vector3[] points = new Vector3[spline.Count];
            for (int i = 0; i < spline.Count; i++)
            {
                points[i] = spline[i].Position; // Уже локальные координаты сплайна
            }
        
            _lineRenderer.positionCount = points.Length;
            _lineRenderer.SetPositions(points);
        }

        [Button]
        private void OnLineDraw()
        {
            if (_lineRenderer == null)
                _lineRenderer = GetComponent<UnityEngine.LineRenderer>();

            DrawLine();
        }
    }
}