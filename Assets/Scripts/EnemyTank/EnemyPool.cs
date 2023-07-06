using BattleTank.Utilities;

namespace BattleTank.EnemyTank
{
    public class EnemyPool : GenericObjectPool<EnemyTankController>
    {
        private EnemyTankView enemyPrefab;
        private EnemyTankModel enemyData;

        public EnemyTankController GetEnemyTank(EnemyTankModel enemyData, EnemyTankView enemyPrefab)
        {
            this.enemyPrefab = enemyPrefab;
            this.enemyData = enemyData;
            return GetItem();
        }

        protected override EnemyTankController CreateItem() => new EnemyTankController(enemyData, enemyPrefab);
    }
}