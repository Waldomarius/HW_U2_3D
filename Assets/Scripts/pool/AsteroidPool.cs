using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


namespace pool
{
    public class AsteroidPool : MonoBehaviour
    {
        private List<GameObject> _pool = new List<GameObject>();
        private List<AsteroidSpawner.AsteroidData> _asteroidData;

        public List<GameObject> GetPoolObjects() => _pool;
        
        public void Initialize(List<AsteroidSpawner.AsteroidData> asteroidData)
        {
            _asteroidData =  asteroidData;
            InitializePool();
        }
        
        private void InitializePool()
        {
            foreach (var data in _asteroidData)
            {
                for (int i = 0; i < data.count; i++)
                {
                    CreateNewObject(Vector3.zero, data.asteroidPrefab);
                }
            }
        }

        private GameObject CreateNewObject(Vector3 position, GameObject prefab)
        {
            GameObject newObj = Instantiate(prefab, position, Quaternion.identity);
            newObj.SetActive(false);
            _pool.Add(newObj);
            return newObj;
        }
        
        public GameObject GetOrCreateAsteroid(Vector3 position)
        {
            // Сделаем рандомное выпадение из пула
            List<GameObject> freeGo = new List<GameObject>() ;
            foreach (GameObject go in _pool)
            {
                if (!go.activeInHierarchy)
                {
                    freeGo.Add(go);
                }
            }

            // Нет доступных объектов - ничего не возвращаем.
            if (freeGo.Count == 0)
            {
                return null;
            }
            
            int randomGo = Random.Range(0, freeGo.Count);
            GameObject result =  freeGo[randomGo];
            result.transform.position = position;
            result.SetActive(true);
            return result;
        }

        public void ReturnAsteroid(GameObject go)
        {
            go.SetActive(false);
        }
    }
}