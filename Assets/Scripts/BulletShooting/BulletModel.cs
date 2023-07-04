namespace BattleTank.BulletShooting
{
    public class BulletModel
    {
        public BulletType BulletType;
        public int damage;
        public float speed;
        public BulletController BulletController { get; private set; }

        public void SetBulletController(BulletController bulletController)
        {
            BulletController = bulletController;
        }

        public BulletModel(BulletScriptableObject bulletScriptableObject)
        {
            BulletType = bulletScriptableObject.BulletType;
            damage = bulletScriptableObject.damage;
            speed = bulletScriptableObject.speed;
        }
    }
}