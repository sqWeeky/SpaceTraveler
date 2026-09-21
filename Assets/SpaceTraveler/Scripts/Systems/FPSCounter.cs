using TMPro;
using UnityEngine;

namespace SpaceTraveler.Scripts.Systems
{
    public class FPSCounter : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _fpsText;
        [SerializeField] private float _updateInterval = 0.5f;

        private float _accum = 0f;
        private int _frames = 0;
        private float _timeLeft;
        private float _fps;

        private void Start()
        {
            if (_fpsText == null)
            {
                _fpsText = GetComponent<TextMeshProUGUI>();
                _timeLeft = _updateInterval;
            }
        }

        private void Update()
        {
            _timeLeft -= Time.unscaledDeltaTime;
            _accum += Time.unscaledDeltaTime;
            _frames++;

            if (_timeLeft <= 0)
            {
                _fps = _frames / _accum;
                _fpsText.text = $"FPS: {_fps:F1}";

                _fpsText.color = _fps < 30 ? Color.red :_fps < 60 ? Color.yellow : Color.green;

                _timeLeft = _updateInterval;
                _accum = 0f;
                _frames = 0;
            }
        }
    }
}