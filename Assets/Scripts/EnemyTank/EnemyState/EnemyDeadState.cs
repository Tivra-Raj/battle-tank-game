using BattleTank.PlayerTank;
using UnityEngine;

namespace BattleTank.EnemyState
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
            GetEnemyRange();         

            if (TankService.Instance.TankController.TankView.enabled == false)
            {
                Debug.Log(" player died");
                //ChangeEnemyState(enemyTankView.enemyDeadState);
            }
        }
    }
}