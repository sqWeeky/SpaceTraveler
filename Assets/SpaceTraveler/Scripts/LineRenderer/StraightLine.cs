using UnityEngine;

namespace SpaceTraveler.Scripts.LineRenderer
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class StraightLine : MonoBehaviour
    {
        [SerializeField] private Vector3 _endPoint;
        [SerializeField] private Material _roadColor;

        private UnityEngine.LineRenderer _lineRenderer;
        private Vector3 _startPoint;
        private readonly float _width = 0.1f;

        private void Awake()
        {
            _lineRenderer = GetComponent<UnityEngine.LineRenderer>();
            _startPoint = transform.position;
            _endPoint = _startPoint + _endPoint;
            DrawLine();
        }

        private void DrawLine()
        {
            _lineRenderer.startWidth = _width;
            _lineRenderer.endWidth = _lineRenderer.startWidth;
            _lineRenderer.loop = false;
            _lineRenderer.material = new Material(Shader.Find("Sprites/Default"));
            _lineRenderer.startColor = _roadColor.color;
            _lineRenderer.endColor = _lineRenderer.startColor;
            _lineRenderer.SetPositions(new Vector3[] { _startPoint, _endPoint });
        }
    }
}
