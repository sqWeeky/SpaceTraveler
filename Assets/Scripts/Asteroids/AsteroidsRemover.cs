using UnityEngine;

public class AsteroidsRemover : MonoBehaviour
{
    [SerializeField] private PoolAsteroids _poolAsteroids;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.TryGetComponent(out Asteroid asteroid))
            _poolAsteroids.Put(asteroid);
    }
}
