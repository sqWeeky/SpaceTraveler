using System;
using UnityEngine;
using DG.Tweening;

public class BlackHole : MonoBehaviour
{
    [SerializeField] private Transform _transformCircle;
    [SerializeField] private Collider _sphereColliderCircle;

    [Space]
    [SerializeField] private float _delay;
    [SerializeField] private int _repeats;
    [SerializeField] private Vector3 _startSize;
    [SerializeField] private Vector3 _endSize;

    private Sequence _sequence;
    
    private void Awake()
    {
        _transformCircle.localScale = _startSize;
    }

    private void Start()
    {
        if (_transformCircle == null)
            return;

        if (_sphereColliderCircle == null)
            return;

        _transformCircle.DOScale(_endSize, _delay).SetEase(Ease.Linear).SetLoops(_repeats, LoopType.Yoyo);
    }
}