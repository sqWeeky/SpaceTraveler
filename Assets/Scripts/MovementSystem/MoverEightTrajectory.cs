using UnityEngine;
using static UnityEditor.Searcher.SearcherWindow.Alignment;

public class MoverEightTrajectory : MonoBehaviour
{
    [SerializeField] private float _radius = 5f;
    [SerializeField] private float _speed = 2f;
    [SerializeField] private bool _isHorizontal = true;

    private float _time;
    private Vector3 _startPosition;

    private void Awake()
    {
        _startPosition = transform.position;
    }

    private void Update()
    {
        _time += _speed * Time.deltaTime;

        float a = _radius * Mathf.Sin(_time);
        float b = _radius * Mathf.Sin(_time) * Mathf.Cos(_time);

        // Выбираем плоскость движения
        if (_isHorizontal)
        {
            // Горизонтальная восьмерка (движение по XZ)
            transform.position = _startPosition + new Vector3(a, 0, b);
        }
        else
        {
            // Вертикальная восьмерка (движение по YZ)
            transform.position = _startPosition + new Vector3(b, 0, a);
        }
    }
}