using bulletsControl;
using DefaultNamespace.interfaces;
using UnityEngine;

namespace bullets
{
    public abstract class BaseBullet : MonoBehaviour, IBullet
    {
        internal BulletType _type;
        internal float _damage = 10f;
        internal float _velocity = 10f;
        internal float _penetration = 5f;
        internal GameObject _bulletPrefab;
        
        public BulletType Type => _type;
        public float Damage => _damage;
        public float Velocity => _velocity;
        public float Penetration => _penetration;
        public GameObject BulletPrefab => _bulletPrefab;
        
        public void Use(Vector3 position, Vector3 direction)
        {
            if (_bulletPrefab != null)
            {   
                var bulletObj = Instantiate(BulletPrefab, position, Quaternion.identity);
                var bulletComponent = bulletObj.GetComponent<BulletController>();
                if (bulletComponent != null)
                {
                    bulletComponent.Initialize(this, direction);
                }
            }
        }
    }
}