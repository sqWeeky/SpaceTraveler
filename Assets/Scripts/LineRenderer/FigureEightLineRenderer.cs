using UnityEngine;

namespace LineRenderer
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class FigureEightLineRenderer : MonoBehaviour
    {
        [SerializeField] private float _radius = 5f;
        [SerializeField] private bool _isHorizontal = true;
        [SerializeField] private int _segments = 30;

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
                float t = (float)i / _segments * Mathf.PI * 2;
                float a = _radius * Mathf.Sin(t);
                float b = _radius * Mathf.Sin(t) * Mathf.Cos(t);

                Vector3 point;
                if (_isHorizontal)
                    point = _startPosition + new Vector3(a, 0, b);
                else
                    point = _startPosition + new Vector3(b, 0, a);

                _lineRenderer.SetPosition(i, point);
            }
        }

        private Color GetColorLine() => new(Random.value, Random.value, Random.value, 0.3f);
    }
}
