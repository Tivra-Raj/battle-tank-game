using BattleTank.PlayerTank;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    public class EnemyTankController
    {
        public EnemyTankModel EnemyTankModel { get; private set; }
        public EnemyTankView EnemyTankView { get; private set; }

        public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView)
        {
            EnemyTankModel = _enemyTankModel;
            EnemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);

            EnemyTankModel.SetEnemyTankController(this);
            EnemyTankView.SetEnemyTankController(this);
        }
    }
}