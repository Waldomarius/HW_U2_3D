
namespace factory.bullets
{
    public class LaserBullet : BaseBullet
    {
        public LaserBullet(BulletFactory.BulletData data)
        {
            _type =  data.type;
            _damage = data.damage;
            _velocity = data.velocity;
            _penetration = data.penetration;
            _bulletPrefab = data.bulletPrefab;
        }
    }
}