using System.Collections.Generic;
using DefaultNamespace.interfaces;
using factory;
using UnityEngine;

namespace pool
{
    public class BulletPool : IBulletPool
    {
        private readonly BulletFactory _bulletFactory;
        private readonly Dictionary<BulletType, Queue<IBullet>> _pool;

        public BulletPool(BulletFactory bulletFactory)
        {
            _bulletFactory = bulletFactory;
            _pool = new Dictionary<BulletType, Queue<IBullet>>();
        }

        /**
         * Создаем предефайненое количество пуль нужного типа.
         */
        public void Preload(BulletType bulletType, int count)
        {
            for (int i = 0; i < count; i++)
            {
                var bullet = _bulletFactory.CreateBullet(bulletType);
                
                ReturnBullet(bullet);
            }
        }
        
        /**
         * Возвращаем пулю в пул.
         */
        public void ReturnBullet(IBullet bullet)
        {
            if (bullet == null)
            {
                return;
            }
            
            var bulletType = bullet.Type;
            if (!_pool.ContainsKey(bulletType))
            {
                _pool[bulletType] = new Queue<IBullet>();
            }
            
            Queue<IBullet> queue = _pool[bulletType];
            Debug.Log($"Bullet Pool: Enqueue bullet: {bulletType}");
            queue.Enqueue(bullet);
        }
        
        /**
         * Получаем пулю из пула.
         */
        public IBullet GetBullet(BulletType bulletType)
        {
            if (!_pool.ContainsKey(bulletType))
            {
                _pool[bulletType] = new Queue<IBullet>();
            }
            
            Queue<IBullet> queue = _pool[bulletType];

            if (queue.Count > 0)
            {
                Debug.Log($"Bullet Pool: Dequeue bullet: {bulletType}");
                return queue.Dequeue();
            }
            
            // Создадим и вернем новую пулю, если пули в пуле закончились.
            return _bulletFactory.CreateBullet(bulletType);
        }
    }
}