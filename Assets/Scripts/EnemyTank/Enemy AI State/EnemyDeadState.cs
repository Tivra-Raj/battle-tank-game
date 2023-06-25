using BattleTank.PlayerTank;
using UnityEngine;

namespace BattleTank.EnemyTank.Enemy_AI_State
{
    public class EnemyDeadState : EnemyTankState
    {
        [SerializeField] private GameObject explosion;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            explosion = Instantiate(explosion, enemyTankView.transform.position, Quaternion.identity);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            if (TankService.Instance.TankController.TankView.enabled == false)
            {
                Debug.Log(" player died");
                enemyTankView.ChangeEnemyState(enemyTankView.enemyDeadState);
            }
        }
    }
}