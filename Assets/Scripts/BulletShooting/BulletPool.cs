using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        private BulletView bulletPrefab;
        private BulletModel bulletModel;
        private Vector3 bulletPosition;
        private Quaternion bulletRotation;

        public BulletController GetBullet(BulletView bulletPrefab, BulletModel bulletModel, Vector3 bulletPosition, Quaternion bulletRotation)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletModel = bulletModel;
            this.bulletPosition = bulletPosition;
            this.bulletRotation = bulletRotation;

            return GetItem();
        }

        protected override BulletController CreateItem() => new BulletController(bulletModel, bulletPrefab, bulletPosition, bulletRotation);
    }
}