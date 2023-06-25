using UnityEngine;

namespace BattleTank.EnemyTank.Enemy_AI_State
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
    }
}
