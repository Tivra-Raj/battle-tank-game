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

            EnemyTankController enemyTankController = GetItem();
            enemyTankController.EnemyTankView.gameObject.SetActive(true);
            return enemyTankController;
        }

        protected override EnemyTankController CreateItem() => new EnemyTankController(enemyData, enemyPrefab);
    }
}