using BattleTank.Utilities;

namespace BattleTank.BulletShooting
{
    public class BulletService : MonoSingletonGeneric<BulletService>
    {
        public BulletScriptableObject ConfigBullet;

        public BulletController BulletController { get; private set; }

        public BulletController CreateNewBullet()
        {
            BulletScriptableObject bulletScriptableObject = ConfigBullet;

            BulletModel bulletModel = new BulletModel(bulletScriptableObject);
            BulletController = new BulletController(bulletModel, bulletScriptableObject.BulletView);

            return BulletController;
        }
    }
}