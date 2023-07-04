using BattleTank.Utilities;

namespace BattleTank.BulletShooting
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletModel bulletModel;

        public BulletController GetBullet(BulletView bulletPrefab, BulletModel bulletModel)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletModel = bulletModel;

            return GetItem();
        }

        protected override BulletController CreateItem() => new BulletController(bulletModel, bulletPrefab);
    }
}