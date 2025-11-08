using UnityEngine;

namespace DefaultNamespace.interfaces
{
    public class BulletInterfaces
    {
        
    }
    
    public interface IBullet
    {
        BulletType Type { get; }
        float Damage { get; }
        float Velocity { get; }
        float Penetration { get; }
        GameObject BulletPrefab { get; }
        void Use(Vector3 position, Vector3 direction);
    }

    public interface IBulletPool
    {
        IBullet GetBullet(BulletType bulletType);
        void ReturnBullet(IBullet bullet);
        void Preload(BulletType bulletType, int count);
    }

    public interface IBulletFactory
    {
        IBullet CreateBullet(BulletType bulletType);
    }

    public enum BulletType
    {
        Laser
    }
}