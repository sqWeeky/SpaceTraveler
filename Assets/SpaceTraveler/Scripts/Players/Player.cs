using System;
using Reflex.Attributes;
using Reflex.Extensions;
using Reflex.Injectors;
using SpaceTraveler.Scripts.Configs.Skins;
using SpaceTraveler.Scripts.Managers;
using SpaceTraveler.Scripts.Save;
using SpaceTraveler.Scripts.Skins;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace SpaceTraveler.Scripts.Players
{
    public class Player : MonoBehaviour
    {
        [SerializeField] private int _health;
        [SerializeField] private DataShipConfig _shipConfig;
        [SerializeField] private Collider _collider;

        private PlayerData _playerData;

        public event Action OnPlayerDied;
        public event Action OnLevelComplete;

        public void Init()
        {
            GameObjectInjector.InjectSingle(gameObject, SceneManager.GetActiveScene().GetSceneContainer());
            _shipConfig = _playerData.UnlockedPlayerShips[0];
            _health = _shipConfig.MaxHealth;
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
               
                Debug.Log($"Added {amount} stars");
            }
            else
            {
                Debug.LogError("Amount < 0");
            }
        }
    }
}