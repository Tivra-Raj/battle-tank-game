using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.EnemyTank
{
    public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
    {

        public EnemyTankScriptableObject[] ConfigEnemyTank;
        public EnemyTankController EnemyTankController { get; private set; }
        private EnemyPool enemyPool;

        void Start()
        {
            enemyPool = GetComponent<EnemyPool>();
            CreateNewTank();
        }

        private EnemyTankController CreateNewTank()
        {
            int pickRandomTank = Random.Range(0, ConfigEnemyTank.Length);
            EnemyTankScriptableObject enemyTankScriptableObject = ConfigEnemyTank[pickRandomTank];

            EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankScriptableObject);
            EnemyTankController = enemyPool.GetEnemyTank(enemyTankModel, enemyTankScriptableObject.EnemyTankView);

            return EnemyTankController;
        }
    }
}