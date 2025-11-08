using DefaultNamespace.interfaces;
using factory;
using pool;
using UnityEngine;

namespace bulletsControl
{
    public class BulletComponent : MonoBehaviour
    {
        private IBulletPool _bulletPool;
        [SerializeField] private BulletFactory _bulletFactory;
        
        public IBullet GetBullet(BulletType bulletType) => _bulletPool.GetBullet(bulletType);
        public void ReturnBullet(IBullet bullet) => _bulletPool.ReturnBullet(bullet);
        
        public void Initialize()
        {
            _bulletFactory.Initialize();
            _bulletPool = new BulletPool(_bulletFactory);
            
            _bulletPool.Preload(BulletType.Laser, 15);
        }

        public void ShootBullet(BulletType bulletType, Vector3 position, Vector3 direction)
        {
            IBullet bullet = GetBullet(bulletType);
            bullet.Use(position, direction);
        }
    }
}