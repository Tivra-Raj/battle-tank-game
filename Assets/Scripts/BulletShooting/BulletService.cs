using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletService : MonoSingletonGeneric<BulletService>
    {
        [SerializeField] private BulletPool bulletPool;
        public BulletController GetBullet(Vector3 bulletPosition, Quaternion bulletRotation, BulletScriptableObject configBulletType)
        {
            BulletController bulletController = bulletPool.GetBullet(configBulletType.BulletView, bulletPosition, bulletRotation);
            return bulletController;
        }

        public void ReturnBulletToPool(BulletController bulletToReturn) => bulletPool.ReturnItem(bulletToReturn);
    }
}