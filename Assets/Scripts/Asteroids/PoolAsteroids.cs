using System.Collections.Generic;
using UnityEngine;

public class PoolAsteroids : MonoBehaviour
{
    [SerializeField] private Transform _container;
    [SerializeField] private List<Asteroid> _prefabs;

    private Queue<Asteroid> _poolAsteroids;

    public IEnumerable<Asteroid> PooledEnemes => _poolAsteroids;

    private void Awake()
    {
        _poolAsteroids = new Queue<Asteroid>();
    }

    public Asteroid Get()
    {
        if (_poolAsteroids.Count == 0)
        {
            var asteroid = Instantiate(_prefabs[Random.Range(0, _prefabs.Count - 1)]);
            asteroid.transform.parent = _container;
            asteroid.gameObject.SetActive(true);
            return asteroid;
        }

        var asteroid_2 = _poolAsteroids.Dequeue();
        asteroid_2.gameObject.SetActive(true);
        return asteroid_2;
    }

    public void Put(Asteroid asteroid)
    {
        _poolAsteroids.Enqueue(asteroid);
        asteroid.gameObject.SetActive(false);
    }

    public void Reset() => _poolAsteroids.Clear();
}
