using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace asteroid
{
    public class AsteroidMoveComponent : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _rotation;
        
        public Action<GameObject> IsOutBoundary;
        
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
                IsOutBoundary?.Invoke(gameObject);
            }
        }
    }
}