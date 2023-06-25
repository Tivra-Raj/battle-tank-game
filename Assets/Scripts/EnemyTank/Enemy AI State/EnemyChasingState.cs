using UnityEngine;
using BattleTank.PlayerTank;

namespace BattleTank.EnemyTank.Enemy_AI_State
{
    public class EnemyChasingState : EnemyTankState
    {

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("entering : chashing state");
            enemyTankView.GetNavMeshAgent().SetDestination(TankService.Instance.TankController.TankView.transform.position);
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("exiting : chashing state");
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetSightRange(), enemyTankView.GetPlayerLayerMask());
            playerInAttackRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetAttackRange(), enemyTankView.GetPlayerLayerMask());

            if (!playerInSightRange && !playerInAttackRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyPatrolingState);
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyAttackingState);
            }
        }
    }
}