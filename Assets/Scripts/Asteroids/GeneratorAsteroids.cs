using System.Collections;
using UnityEngine;

public class GeneratorAsteroids : MonoBehaviour
{
    [SerializeField] private float _delay;
    [SerializeField] private float _upperBound;
    [SerializeField] private float _lowerBound;
    [SerializeField] private PoolAsteroids _pool;

    private void Start()
    {
        StartCoroutine(GenerateObject());
    }

    private IEnumerator GenerateObject()
    {
        var wait = new WaitForSeconds(_delay);

        Spawn();

        yield return wait;

        StartCoroutine(GenerateObject());
    }


    private void Spawn()
    {
        float spawnPositionX = Random.Range(transform.position.x + _upperBound, transform.position.x + _lowerBound);
        float spawnPositionZ = Random.Range(transform.position.z + _upperBound, transform.position.z + _lowerBound);
        Vector3 spawnPoint = new Vector3(spawnPositionX, transform.position.y, spawnPositionZ);

        var newObject = _pool.Get();

        newObject.transform.position = spawnPoint;
    }
}
