using UnityEngine;

public class MoverInCircle : MonoBehaviour
{
    [SerializeField] private float _radius = 0.07f;
    [SerializeField] private float _speed = 0.5f;
    [SerializeField] private Transform _positionCenter;

    private float _angle = 0f;
    private Transform _transform;

    private void Awake()
    {
        _transform = transform;
    }

    private void Update()
    {
        _angle +=  Time.deltaTime;
        float x = _positionCenter.position.x + Mathf.Cos(_angle) * _radius * _speed;
        float z = _positionCenter.position.z + Mathf.Sin(_angle) * _radius * _speed;
        _transform.position = new Vector3(x, _transform.position.y, z);
    }
}
