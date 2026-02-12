using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletPool : GenericObjectPool<BulletController>
    {
        [SerializeField] private BulletScriptableObject configBullet;
        private BulletView bulletPrefab;
        private Vector3 bulletPosition;
        private Quaternion bulletRotation;

        public BulletController GetBullet(BulletView bulletPrefab, Vector3 bulletPosition, Quaternion bulletRotation)
        {
            this.bulletPrefab = bulletPrefab;
            this.bulletPosition = bulletPosition;
            this.bulletRotation = bulletRotation;

            BulletController bulletController = GetItem();
            bulletController.SetPosition(bulletPosition, bulletRotation);
            bulletController.BulletView.gameObject.SetActive(true);
            return bulletController;
        }

        protected override BulletController CreateItem()
        {
            BulletModel bulletModel = new BulletModel(configBullet);
            BulletController bulletController= new BulletController(bulletModel, bulletPrefab, bulletPosition, bulletRotation);
            return bulletController;
        }
    }
}