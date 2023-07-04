using BattleTank.PlayerTank;
using UnityEngine;

namespace BattleTank.EnemyTank
{
    public class EnemyTankController
    {
        private int currentHealth;

        public EnemyTankModel EnemyTankModel { get; private set; }
        public EnemyTankView EnemyTankView { get; private set; }

        public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView)
        {
            EnemyTankModel = _enemyTankModel;
            EnemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);

            EnemyTankModel.SetEnemyTankController(this);
            EnemyTankView.SetEnemyTankController(this);
        }

        public void Configure(Vector3 setPosition)
        {
           EnemyTankView.transform.position = setPosition;   
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
                DestroyEnemy();
        }

        public void OnEnemyCollided(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<TankView>() != null)
            {
                TankService.Instance.TankController.TakeDamage(EnemyTankModel.damageToInflict);
                DestroyEnemy();
            }
        }

        private void DestroyEnemy()
        {
            GameService.GameService.Instance.GetUIService().IncrementScore(EnemyTankModel.scoreToGrant);
            EnemyTankView.gameObject.SetActive(false);
            EnemyTankService.Instance.ReturnEnemyToPool(this);
        }
    }
}