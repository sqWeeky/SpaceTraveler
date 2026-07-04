using SpaceTraveler.Scripts.Players;
using UnityEngine;

namespace SpaceTraveler.Scripts.Enemies
{
    [RequireComponent(typeof(Collider))]
    public class Enemy : MonoBehaviour
    {
        [SerializeField] private int _damage;

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player)) 
                player.TakeDamage(_damage);
        }
    }
}