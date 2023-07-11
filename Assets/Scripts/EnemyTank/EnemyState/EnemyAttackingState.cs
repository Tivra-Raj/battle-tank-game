using UnityEngine;

namespace BattleTank.EnemyState
{
    public class EnemyAttackingState : EnemyTankState
    {
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyTankView.activeState = EnemyStates.Attacking;
            Debug.Log("Enter Attacking state");
            enemyTankView.EnemyTankController.Attacking();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            SetEnemyRange();
            enemyTankView.EnemyTankController.Attacking();

            if (!playerInAttackRange && !playerInSightRange)
            {
                ChangeEnemyState(enemyTankView.enemyPatrolingState);
            }
            else if(!playerInAttackRange && playerInSightRange)
            {
                ChangeEnemyState(enemyTankView.enemyChasingState);
            }
        }
    }
}