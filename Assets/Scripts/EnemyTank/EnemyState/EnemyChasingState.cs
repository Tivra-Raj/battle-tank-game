namespace BattleTank.EnemyState
{
    public class EnemyChasingState : EnemyTankState
    {

        public override void OnStateEnter()
        {
            base.OnStateEnter();
            enemyTankView.EnemyTankController.Chasing();
            enemyTankView.activeState = EnemyStates.Chasing;
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
        }

        private void Update()
        {
            GetEnemyRange();

            enemyTankView.EnemyTankController.Chasing();

            if (!playerInSightRange && !playerInAttackRange)
            {
                ChangeEnemyState(enemyTankView.enemyPatrolingState);
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                ChangeEnemyState(enemyTankView.enemyAttackingState);
            }
        }
    }
}