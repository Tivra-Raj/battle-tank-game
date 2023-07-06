using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletService : MonoSingletonGeneric<BulletService>
    {
        public BulletScriptableObject CofigBullet;
        public void CreateNewBullet(Vector3 bulletPosition, Quaternion bulletRotation, BulletScriptableObject configBulleType)
        {
            BulletScriptableObject bulletScriptableObject = configBulleType;
            BulletModel bulletModel = new BulletModel(bulletScriptableObject);
            BulletController bulletController = new BulletController(bulletModel, bulletScriptableObject.BulletView, bulletPosition, bulletRotation);
        }
    }
}