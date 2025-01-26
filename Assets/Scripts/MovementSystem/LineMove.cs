using DG.Tweening;
using UnityEngine;

public class LineMove : MonoBehaviour
{
    [SerializeField] private Vector3 _distance;
    [SerializeField] private float _duration;
    [SerializeField] private bool _loop = true;

    private Vector3 _nextPosition;

    private void Awake()
    {
        _nextPosition = transform.position + _distance;
    }

    private void Start()
    {
        transform.DOMove(_nextPosition, _duration)
            .SetEase(Ease.Linear)
            .SetLoops(_loop ? -1 : 0, LoopType.Yoyo);
    }
}
