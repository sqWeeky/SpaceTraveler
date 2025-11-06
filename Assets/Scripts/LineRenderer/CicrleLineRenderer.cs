using UnityEngine;

namespace LineRenderer
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class CicrleLineRenderer : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private int _segments = 30;
        [SerializeField] private Transform _positionCenter;

        private UnityEngine.LineRenderer _lineRenderer;
        private Vector3 _startPosition;
        private float _width = 0.1f;

        private void Awake()
        {
            _lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            _startPosition = transform.position;
            DrawLine();
        }

        private void DrawLine()
        {
            _lineRenderer.positionCount = _segments + 1;
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _lineRenderer.startWidth;
            _lineRenderer.loop = false;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = GetColorLine();
            _lineRenderer.endColor = _lineRenderer.startColor;

            for (int i = 0; i <= _segments; i++)
            {
                float angle = (float)i / _segments * Mathf.PI * 2f;
                float x = Mathf.Cos(angle) * _radius;
                float z = Mathf.Sin(angle) * _radius;
                _lineRenderer.SetPosition(i, new Vector3(x + _startPosition.x, _startPosition.y, z + _startPosition.z));
            }
        }

        private Color GetColorLine() => new(Random.value, Random.value, Random.value, 0.3f);
    }
}
