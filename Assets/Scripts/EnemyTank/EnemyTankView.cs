using BattleTank.EnemyState;
using BattleTank.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyTankView : MonoBehaviour, IDamagable
    {
        private NavMeshAgent NavMeshAgent;

        [SerializeField] public EnemyPatrolingState enemyPatrolingState;
        [SerializeField] public EnemyChasingState enemyChasingState;
        [SerializeField] public EnemyAttackingState enemyAttackingState;
        [SerializeField] public EnemyDeadState enemyDeadState;

        [SerializeField] private EnemyStates initialState;
        public EnemyStates activeState;
        public EnemyTankState enemyTankCurrentState;

        [SerializeField] public Transform turetTransform;

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
            enemyTankCurrentState.ChangeEnemyState(enemyPatrolingState);
        }

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

        

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, EnemyTankController.EnemyTankModel.sightRange);

            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(transform.position, EnemyTankController.EnemyTankModel.attackRange);
        }

        public void TakeDamage(int damageToTake) => EnemyTankController.TakeDamage(damageToTake);
    }
}