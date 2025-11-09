using System;
using System.Collections.Generic;
using asteroid;
using pool;
using UnityEngine;
using Random = UnityEngine.Random;

public class AsteroidSpawner : MonoBehaviour
{
    [Serializable]
    public class AsteroidData
    {
        public GameObject asteroidPrefab;
        public int count = 10;
    }
    
    [SerializeField] private int xMinPosition = -10;
    [SerializeField] private int xMaxPosition = 10;
        
    [SerializeField] private List<AsteroidData> _asteroidData;
    
    private AsteroidPool _pool;

    private void OnEnable()
    {
        foreach (var go in _pool.GetPoolObjects())
        {
            AsteroidMoveComponent component = go.GetComponent<AsteroidMoveComponent>();
            // Подписка на слушателя
            component.IsOutBoundary += ReturnAsteroid;
        }
    }

    private void Awake()
    {   
        _pool = GetComponent<AsteroidPool>();
        _pool.Initialize(_asteroidData);
        Invoke(nameof(SpawnAsteroid), 1f);
    }

    private void SpawnAsteroid()
    {
        int randomXPosition = Random.Range(xMinPosition, xMaxPosition);
        Vector3 position = new Vector3(randomXPosition, transform.position.y, transform.position.z);
        _pool.GetOrCreateAsteroid(position);
        Invoke(nameof(SpawnAsteroid), 1f);
    }

    private void ReturnAsteroid(GameObject go)
    {
        _pool.ReturnAsteroid(go);
    }
    
    private void OnDisable()
    {
        foreach (var go in _pool.GetPoolObjects())
        {
            AsteroidMoveComponent component = go.GetComponent<AsteroidMoveComponent>();
            // Отписка на слушателя
            component.IsOutBoundary -= ReturnAsteroid;
        }
    }
}