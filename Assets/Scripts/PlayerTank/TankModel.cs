using BattleTank.BulletShooting;

namespace BattleTank.PlayerTank
{
    public class TankModel
    {
        public float MovementSpeed;
        public float RotationSpeed;
        public float Health;
        public int BulletsFired;
        public int EnemiesKilled;
        public float DistanceTravelled = 0;
        public float FireRate;
        public TankType TankType;
        public BulletScriptableObject BulletType;

        public TankController TankController { get; private set; }

        public void SetTankController(TankController tankController)
        {
            TankController = tankController;
        }

        public TankModel(TankScriptableObject tankScriptableObject)
        {
            TankType = tankScriptableObject.TankType;
            MovementSpeed = tankScriptableObject.MovementSpeed;
            RotationSpeed = tankScriptableObject.RotationSpeed;
            Health = tankScriptableObject.Health;
            FireRate = tankScriptableObject.FireRate;
            BulletType = tankScriptableObject.BulletType;
        }
    }
}