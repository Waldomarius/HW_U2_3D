using System;
using System.Collections.Generic;
using DefaultNamespace.interfaces;
using factory.bullets;
using UnityEngine;

namespace factory
{
    public class BulletFactory : MonoBehaviour, IBulletFactory
    {
        [Serializable]
        public class BulletData
        {
            public BulletType type;
            public float damage = 10f;
            public float velocity = 10f;
            public float penetration = 5f;
            public GameObject bulletPrefab;
        }

        [Header("Bullet Data")]
        [SerializeField] private List<BulletData> _bulletData = new List<BulletData>();
        
        private Dictionary<BulletType, BulletData> _bulletDatatGroupDictionary;

        public void Initialize()
        {
            _bulletDatatGroupDictionary = new Dictionary<BulletType, BulletData>();
            
            // Заполнение словарей
            foreach (BulletData data in _bulletData)
            {
                _bulletDatatGroupDictionary[data.type] = data;
            }
        }
        
        public IBullet CreateBullet(BulletType type)
        {
            return type switch
            {
                BulletType.Laser => CreateLaserBullet(),
                _ => throw new ArgumentException("Unknown type.")
            };
        }

        private IBullet CreateLaserBullet()
        {
            return new LaserBullet(_bulletDatatGroupDictionary[BulletType.Laser]);
        }
    }
}