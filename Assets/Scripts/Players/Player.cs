using System;
using UnityEngine;

namespace Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private int _scoreStars;
        [SerializeField] private GameObject _skin;
        [SerializeField] private Collider _collider;

        public int ScoreStars => _scoreStars;

        public event Action OnPlayerDied;
        public event Action OnLevelComplete;

        public void Init(int scoreStars, int healthPoint, GameObject skin)
        {
            _scoreStars = scoreStars;
            _health = healthPoint;
            _skin = skin;
        }

        public void TakeDamage(int damage)
        {
            if (damage < 0)
                damage = 0;

            _health -= damage;
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
    }
}