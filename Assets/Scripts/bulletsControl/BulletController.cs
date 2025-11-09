using DefaultNamespace.interfaces;
using factory.bullets;
using UnityEngine;

namespace bulletsControl
{
    public class BulletController : MonoBehaviour
    {
        private IBullet _bulletData;
        private Vector3 _direction;
        private float _timer;
        private Rigidbody _rigidbody;
        
        private GameObject _bulletSpawner;
        private BulletComponent _bulletComponent;
        
        public void Initialize(BaseBullet baseBullet, Vector3 direction)
        {
            _bulletData = baseBullet;
            _direction = direction;
        }
        
        private void Start()
        {            
            _bulletSpawner = GameObject.Find("BulletSpawner");
            _bulletComponent = _bulletSpawner.GetComponent<BulletComponent>();
            _rigidbody = GetComponent<Rigidbody>();
            transform.Rotate(new Vector3 (90, 0, 0));
        }

        private void LateUpdate()
        {
            if (_rigidbody != null)
            {
                _rigidbody.velocity = _direction * _bulletData.Velocity;
            }
        }
        
        private void OnTriggerExit(Collider other)
        {
            GameObject go = other.gameObject;
            if (go.tag is "Boundary" && gameObject.tag is "Bullet")
            {
                _bulletComponent.ReturnBullet(_bulletData);
                Destroy(gameObject);
                Debug.Log("OnTriggerExit: Destroy bullet gameobject and return to pool");
            }

        }
    }
}