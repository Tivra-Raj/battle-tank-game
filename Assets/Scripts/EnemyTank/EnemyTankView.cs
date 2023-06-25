using BattleTank.EnemyTank.Enemy_AI_State;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyTankView : MonoBehaviour
    {
        private NavMeshAgent NavMeshAgent;
        private EnemyTankState enemyTankCurrentState;

        [SerializeField] public EnemyPatrolingState enemyPatrolingState;
        [SerializeField] public EnemyChasingState enemyChasingState;
        [SerializeField] public EnemyAttackingState enemyAttackingState;
        [SerializeField] public EnemyDeadState enemyDeadState;

        public EnemyTankController EnemyTankController { get; private set; }

        public void SetEnemyTankController(EnemyTankController enemyTankController)
        {
            EnemyTankController = enemyTankController;
        }

        private void Awake()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
           
        }

        private void Start()
        {
            ChangeEnemyState(enemyPatrolingState);
        }

        private void Update() {}

        public NavMeshAgent GetNavMeshAgent()
        {
            return NavMeshAgent;
        }

        public float GetWalkPointRange()
        {
            return EnemyTankController.EnemyTankModel.WalkPointRange;
        }

        public float GetSightRange()
        {
            return EnemyTankController.EnemyTankModel.sightRange;
        }

        public float GetAttackRange()
        {
            return EnemyTankController.EnemyTankModel.attackRange;
        }

        public LayerMask GetPlayerLayerMask()
        {
            return EnemyTankController.EnemyTankModel.PlayerLayerMask;
        }

        public void ChangeEnemyState(EnemyTankState newState)
        {
            if(enemyTankCurrentState != null)
            {
                Debug.Log("Enemy state : " + newState);
                enemyTankCurrentState.OnStateExit();
            }

            enemyTankCurrentState = newState;
            enemyTankCurrentState.OnStateEnter();
        }

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyTankController.EnemyTankModel.sightRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, EnemyTankController.EnemyTankModel.attackRange);
        }
    }
}