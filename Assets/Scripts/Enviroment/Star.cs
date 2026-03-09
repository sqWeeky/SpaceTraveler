using Players;
using UnityEngine;

namespace Enviroment
{
    public class Star : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player))
            {
                player.AddStars(1);
                Destroy(gameObject);
            }
        }
    }
}