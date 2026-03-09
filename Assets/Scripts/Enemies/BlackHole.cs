using DG.Tweening;
using UnityEngine;

namespace Enemies
{
    public class BlackHole : MonoBehaviour
    {
        [SerializeField] private Transform _transformCircle;
        [SerializeField] private Collider _sphereColliderCircle;

        [Space]
        [SerializeField] private float _delay;
        [SerializeField] private int _repeats;
        [SerializeField] private Vector3 _startSize;
        [SerializeField] private Vector3 _endSize;
        [SerializeField] private float _timeStop;
    
        private Sequence _sequence;
    
        private void Awake()
        {
            _transformCircle.localScale = _startSize;
        }

        private void Start()
        {
            StartPulsing();
        }
    
        private void OnDestroy()
        {
            _sequence?.Kill();
        }

        private void StartPulsing()
        {
            if (_transformCircle == null)
                return;

            if (_sphereColliderCircle == null)
                return;
        
            _sequence?.Kill();

            _sequence = DOTween.Sequence();

            _sequence.Append(_transformCircle.DOScale(_endSize, _delay).SetEase(Ease.InOutSine))
                .AppendInterval(_timeStop)
                .Append(_transformCircle.DOScale(_startSize, _delay).SetEase(Ease.InOutSine))
                .AppendInterval(_timeStop)
                .SetLoops(_repeats);
        }
    }
}