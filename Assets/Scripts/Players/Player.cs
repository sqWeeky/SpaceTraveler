using System;
using Managers;
using Systems;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private HealthSystem _health;
        [SerializeField] private GameObject _skin;

        private int _scoreStars;

        public int ScoreStars => _scoreStars;

        public event Action OnPlayerDied;
        public event Action OnLevelComplete;

        public void Init(int scoreStars,int healthPoint, GameObject skin)
        {
            _scoreStars = scoreStars;
            _skin = skin;
        }

        public void AddStars(int amount)
        {
            if (amount > 0)
            {
                _scoreStars += amount;
                Debug.Log($"Added {amount} stars to {_scoreStars}");
            }
            else
            {
                Debug.LogError("Amount < 0");
            }
        }

        // public static Player Instance { get; private set; }
        //
        // private void Awake()
        // {
        //     if (Instance != null && Instance != this)
        //     {
        //         Destroy(gameObject);
        //         return;
        //     }
        //
        //     Instance = this;
        //     DontDestroyOnLoad(gameObject);
        // }
    }
}