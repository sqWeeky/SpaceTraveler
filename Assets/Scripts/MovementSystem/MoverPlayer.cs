using System;
using UnityEngine;
using UnityEngine.Splines;

public class MoverPlayer : MonoBehaviour
{
    [SerializeField] private SplineContainer _spline;
    [SerializeField] private float _speed = 10f;

    private float _normalizedDistance = 0f;

    private void Awake()
    {
        if (_spline == null)
            throw new ArgumentException(nameof(_spline));
    }

    private void Update()
    {
        if (Input.GetMouseButton(0))
            MoveAlongSpline();       
    }

    private void MoveAlongSpline()
    {
        //if (_spline == null)  не знаю, где лучше проверять на наличие сплайна???
        //    return;

        _normalizedDistance += (_speed / _spline.Spline.GetLength()) * Time.deltaTime;
        _normalizedDistance = Mathf.Clamp01(_normalizedDistance);

        Vector3 position = _spline.Spline.EvaluatePosition(_normalizedDistance);
        transform.position = position;
        transform.right = _spline.Spline.EvaluateTangent(_normalizedDistance);
    }
}
