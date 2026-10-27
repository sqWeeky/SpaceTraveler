using System;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Data;
using SpaceTraveler.Scripts.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Systems
{
    public enum TimeFormat
    {
        HoursMinutesSeconds,
        MinutesSeconds
    }

    public class TimerCounter : MonoBehaviour
    {
        [Inject] private SaveManager _saveManager;

        [Header("UI")]
        [SerializeField] private TimeFormat _timeFormat = TimeFormat.MinutesSeconds;
        [SerializeField] private TextMeshProUGUI _timerText;

        [Header("Settings")]
        [SerializeField] private string _lvlID;

        private float _elapsedTime;
        private bool _timerActive;
        private LevelData _levelData;

        public float ElapsedTime => _elapsedTime;
        public bool TimerActive => _timerActive;

        public event Action<float> OnTimerStopped;

        private void Start()
        {
            //GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
            GameObjectInjector.InjectObject(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
        }

        public void Init(LevelData levelData)
        {
            if (_saveManager == null)
                Debug.LogError("SaveManager not registered");

            _levelData = levelData;
        }

        private void Update()
        {
            if (!_timerActive)
                return;

            _elapsedTime += Time.deltaTime;
            UpdateUI();
        }

        public void StartTimer()
        {
            _timerActive = true;
        }

        public void PauseTimer()
        {
            _timerActive = false;
        }

        public void StopTimer()
        {
            _timerActive = false;

            _saveManager.SavePlayerTime(_elapsedTime, _levelData);
        }

        public void ResetTimer()
        {
            _elapsedTime = 0f;
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (_timerText != null)
            {
                _timerText.text = FormatTime(_elapsedTime);
            }
        }

        private string FormatTime(float time)
        {
            if (time < 0f)
                time = 0f;

            TimeSpan ts = TimeSpan.FromSeconds(time);
            string s;

            switch (_timeFormat)
            {
                case TimeFormat.HoursMinutesSeconds:
                    s = $"{(int)ts.TotalHours:00}:{ts.TotalMinutes:00}:{ts.TotalSeconds:00}";
                    break;
                case TimeFormat.MinutesSeconds:
                    s = $"{ts.TotalMinutes:00}:{ts.TotalSeconds:00}";
                    break;
                default:
                    s = $"{ts.TotalSeconds:00}";
                    break;
            }

            return s;
            // return format switch
            // {
            //     TimeFormat.HoursMinutesSeconds =>
            //         $"{(int)ts.TotalHours:00}:{ts.TotalMinutes:00}:{ts.TotalSeconds:00}",
            //
            //     TimeFormat.MinutesSeconds => $"{ts.TotalMinutes:00}:{ts.TotalSeconds:00}",
            //
            //     _ => $"{ts.TotalSeconds:00}"
            // };
        }
    }
}