using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace asteroid
{
    public class AsteroidMoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotation;
        
        [SerializeField] private GameObject _explosion;

        public Action<GameObject> returnInPool;
        
        private Rigidbody _rb;

        private void Start()
        {
            _rb  = GetComponent<Rigidbody>();
            // Стартовое вращение астероида
            _rb.angularVelocity = Random.insideUnitSphere * _rotation;
            // Движение астероида (идет со знаком минус т.е. навстречу игроку)
            _rb.velocity = -transform.forward * _speed;
        }

        private void OnTriggerExit(Collider other)
        {
            GameObject go = other.gameObject;
            if (go.tag is "Boundary" && gameObject.tag is "Asteroid")
            {
                returnInPool?.Invoke(gameObject);
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            GameObject go = other.gameObject;
            
            // Астероид столкнулся с игроком или пулей или другим астероидом
            if (gameObject.tag is "Asteroid" && go.tag is "Player" or "Bullet" or "Asteroid")
            {
                Instantiate(_explosion, gameObject.transform.position, gameObject.transform.rotation);
                returnInPool?.Invoke(gameObject);
            }
        }
    }
}