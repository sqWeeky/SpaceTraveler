using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class Line : MonoBehaviour
{
    [SerializeField] private Vector3 _endPoint;

    private LineRenderer _lineRenderer;
    private Vector3 _startPoint;
    private readonly float _width = 0.1f;

    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
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
        _lineRenderer.startColor = GetColorLine();
        _lineRenderer.endColor = _lineRenderer.startColor;
        _lineRenderer.SetPositions(new Vector3[] { _startPoint, _endPoint });
    }

    private Color GetColorLine() => new(Random.value, Random.value, Random.value, 0.3f);
}
