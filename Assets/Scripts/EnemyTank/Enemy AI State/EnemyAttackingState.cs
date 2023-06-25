using UnityEngine;

namespace BattleTank.EnemyTank.Enemy_AI_State
{
    public class EnemyAttackingState : EnemyTankState
    {
        //[SerializeField] private float timeBetweenAttacks;
        //[SerializeField] private bool alreadyAttacked;

        public override void OnStateEnter()
        {
            base.OnStateEnter();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            playerInSightRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetSightRange(), enemyTankView.GetPlayerLayerMask());
            playerInAttackRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetAttackRange(), enemyTankView.GetPlayerLayerMask());

            if (!playerInAttackRange && !playerInSightRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyPatrolingState);
            }
            else if(!playerInAttackRange && playerInSightRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyChasingState);
            }
        }
    }
}