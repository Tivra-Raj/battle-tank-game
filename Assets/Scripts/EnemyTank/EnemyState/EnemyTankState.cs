using BattleTank.EnemyTank;
using UnityEngine;

namespace BattleTank.EnemyState
{
    [RequireComponent(typeof(EnemyTankView))]
    public class EnemyTankState : MonoBehaviour
    {
        [SerializeField] protected EnemyTankView enemyTankView;
        [SerializeField] protected bool playerInSightRange;
        [SerializeField] protected bool playerInAttackRange;

        private void Awake()
        {
            enemyTankView = GetComponent<EnemyTankView>();
        }
        public virtual void OnStateEnter()
        {
            this.enabled = true;
        }

        public virtual void OnStateExit()
        {
            this.enabled = false;
        }

        public void SetEnemyRange()
        {
            playerInSightRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetSightRange(), enemyTankView.GetPlayerLayerMask());
            playerInAttackRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetAttackRange(), enemyTankView.GetPlayerLayerMask());
        }

        public void ChangeEnemyState(EnemyTankState newState)
        {
            if (enemyTankView.enemyTankCurrentState != null)
            {
                enemyTankView.enemyTankCurrentState.OnStateExit();
            }

            enemyTankView.enemyTankCurrentState = newState;
            enemyTankView.enemyTankCurrentState.OnStateEnter();
        }
    }
}
