using DG.Tweening;
using UnityEngine;

public class RotationAnimation : MonoBehaviour
{
    [SerializeField] private Vector3 _rotationAxis = Vector3.up;
    [SerializeField] private float _duration = 5f;
    [SerializeField] private bool _loop = true;

    private readonly float _rotationAngle = 360f;

    private void Start()
    {
        transform.DORotate(_rotationAxis * _rotationAngle, _duration, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(_loop ? -1 : 0);
    }
}
