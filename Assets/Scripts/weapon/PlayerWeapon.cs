using bulletsControl;
using DefaultNamespace.interfaces;
using UnityEngine;

namespace DefaultNamespace.weapon
{
    public class PlayerWeapon: MonoBehaviour
    {
        [SerializeField] private Transform _firePoint;
        [SerializeField] private float _fireRate;
        
        [SerializeField] private BulletComponent _bulletComponent;
        
        private float _nextFire;
        
        private void Start()
        {
            _bulletComponent.Initialize();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1") && Time.time > _nextFire)
            {
                Shoot(BulletType.Laser);
                _nextFire = Time.time + _fireRate;
            }
        }

        private void Shoot(BulletType type)
        {
            _bulletComponent.ShootBullet(type, _firePoint.position, _firePoint.forward);
        }
    }
}