using UnityEngine;

namespace SpaceTraveler.Scripts.MovementSystem
{
    [RequireComponent(typeof(UnityEngine.LineRenderer))]
    public class MoverEnvironment : MonoBehaviour
    {
        [SerializeField] private UnityEngine.LineRenderer _lineRenderer;
        [SerializeField] private float _speed = 10f;
        [SerializeField] private bool _loop = true;

        private float _distance = 0f;
        private float _totalLength;
        private Vector3[] _points;
        private bool _forward = true;
        private Transform _transform;

        private void Start()
        {
            _transform = transform;

            if (_lineRenderer == null)
                _lineRenderer = GetComponent<UnityEngine.LineRenderer>();

            if (_lineRenderer.positionCount < 2)
            {
               enabled = false;
                return;
            }

            _points = new Vector3[_lineRenderer.positionCount];
            _lineRenderer.GetPositions(_points);
            _totalLength = GetTotalLength();
        }

        private void Update()
        {
            MoveAlongLine();
        }

        private void MoveAlongLine()
        {
            float normalizedDistance;

            if (_loop)
            {
                _distance += _speed * Time.deltaTime;
                normalizedDistance = _distance / _totalLength;
                normalizedDistance = Mathf.Clamp01(normalizedDistance);

                if (normalizedDistance >= 1f)
                    _distance = 0f;
            }
            else
            {
                _distance += (_forward ? 1 : -1) * _speed * Time.deltaTime;
                normalizedDistance = _distance / _totalLength;

                if (normalizedDistance >= 1f)
                {
                    normalizedDistance = 1f;
                    _forward = false;
                }
                else if (normalizedDistance <= 0f)
                {
                    normalizedDistance = 0f;
                    _forward = true;
                }            
            }

            _transform.position = GetPointOnLine(normalizedDistance);
        }

        private Vector3 GetPointOnLine(float t)
        {
            float currentLength = t * _totalLength;
            float accumulatedLength = 0f;

            for (int i = 0; i < _points.Length - 1; i++)
            {
                float segmentLength = Vector3.Distance(_points[i], _points[i + 1]);

                if (accumulatedLength + segmentLength >= currentLength)
                {
                    float segmentT = (currentLength - accumulatedLength) / segmentLength;
                    return Vector3.Lerp(_points[i], _points[i + 1], segmentT);
                }

                accumulatedLength += segmentLength;
            }

            return _points[^1];
        }

        private float GetTotalLength()
        {
            float length = 0f;
            for (int i = 0; i < _points.Length - 1; i++)
                length += Vector3.Distance(_points[i], _points[i + 1]);
            return length;
        }
    }
}
