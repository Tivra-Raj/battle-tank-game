using BattleTank.Utilities;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    public class EnemyTankService : MonoSingletonGeneric<EnemyTankService>
    {
        public EnemyTankScriptableObject[] ConfigEnemyTank;
        
        public EnemyTankController EnemyTankController { get; private set; }
        private EnemyPool enemyPool;

        private void Start()
        {
            enemyPool = GetComponent<EnemyPool>();
            for(int i = 0; i < 5; i++)
            {
                SpawnEnemyTank();
            }
        }
        private void SpawnEnemyTank()
        {
            CreateNewTank(CalculateSpawnPosition());
        }

        private EnemyTankController CreateNewTank(Vector3 spawnPosition)
        {
            int pickRandomTank = Random.Range(0, ConfigEnemyTank.Length);
            EnemyTankScriptableObject enemyTankScriptableObject = ConfigEnemyTank[pickRandomTank];

            EnemyTankModel enemyTankModel = new EnemyTankModel(enemyTankScriptableObject);
            EnemyTankController = enemyPool.GetEnemyTank(enemyTankModel, enemyTankScriptableObject.EnemyTankView);

            EnemyTankController.Configure(spawnPosition);

            return EnemyTankController;
        }

        private Vector3 CalculateSpawnPosition()
        {
            // Calculate a random spawn position using NavMesh
            NavMeshTriangulation navMeshData = NavMesh.CalculateTriangulation();

            int randomIndex = Random.Range(0, navMeshData.vertices.Length);
            Vector3 randomPosition = navMeshData.vertices[randomIndex];

            return randomPosition;
        }

        public void ReturnEnemyToPool(EnemyTankController enemyToReturn) => enemyPool.ReturnItem(enemyToReturn);
    }
}