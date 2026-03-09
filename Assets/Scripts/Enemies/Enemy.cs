using Players;
using UnityEngine;

namespace Enemies
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