using NaughtyAttributes;
using UnityEngine;

namespace LineRenderer
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class CircleLineRenderer : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _segments = 30;
        [SerializeField] private Transform _positionCenter;
        [SerializeField] private Material _roadColor;

        private UnityEngine.LineRenderer _lineRenderer;
        private Vector3 _startPosition;
        private readonly float _width = 0.1f;

        private void Awake()
        {
            _lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            _startPosition = transform.position;
            DrawLine();
        }

        [Button]
        private void OnDraw()
        {
            if (_lineRenderer == null) 
                _lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            
            DrawLine();
        }

        private void DrawLine()
        {
            _lineRenderer.positionCount = _segments + 1;
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _lineRenderer.startWidth;
            _lineRenderer.loop = false;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = _roadColor.color;
            _lineRenderer.endColor = _lineRenderer.startColor;

            for (int i = 0; i <= _segments; i++)
            {
                float angle = (float)i / _segments * Mathf.PI * 2f;
                float x = Mathf.Cos(angle) * _radius;
                float z = Mathf.Sin(angle) * _radius;
                _lineRenderer.SetPosition(i, new Vector3(x + _startPosition.x, _startPosition.y, z + _startPosition.z));
            }
        }
    }
}
