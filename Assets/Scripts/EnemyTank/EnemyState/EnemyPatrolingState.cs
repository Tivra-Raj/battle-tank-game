using UnityEngine;

namespace BattleTank.EnemyState
{
    public class EnemyPatrolingState : EnemyTankState
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyTankView.activeState = EnemyStates.Patroling;
            Debug.Log("Enter Patroling state");
            enemyTankView.EnemyTankController.Patroling();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            SetEnemyRange();

            enemyTankView.EnemyTankController.Patroling();

            if (playerInSightRange && !playerInAttackRange)
            {
                ChangeEnemyState(enemyTankView.enemyChasingState);
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                ChangeEnemyState(enemyTankView.enemyAttackingState);
            }
        }
    }
}