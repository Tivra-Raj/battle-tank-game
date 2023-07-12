using BattleTank.EnemyState;
using BattleTank.Interface;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyTankView : MonoBehaviour, IDamagable
    {
        [Header("Enemy states")]
        public EnemyPatrolingState enemyPatrolingState;
        public EnemyChasingState enemyChasingState;
        public EnemyAttackingState enemyAttackingState;
        public EnemyTankState enemyTankCurrentState;

        [SerializeField] private EnemyStates initialState;
        public EnemyStates activeState;

        [Header("Shooting")]
        public Transform turetTransform;

        private NavMeshAgent NavMeshAgent;
        public EnemyTankController EnemyTankController { get; private set; }

        public void SetEnemyTankController(EnemyTankController enemyTankController)
        {
            EnemyTankController = enemyTankController;
        }

        private void Awake() 
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();          
        }

        private void Start() => InitializeEnemyTankState();

        public NavMeshAgent GetNavMeshAgent()
        {
            return NavMeshAgent;
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

        private void InitializeEnemyTankState()
        {
            switch (initialState)
            {
                case EnemyStates.Patroling:
                    enemyTankCurrentState = enemyPatrolingState;
                    break;
                case EnemyStates.Chasing:
                    enemyTankCurrentState = enemyChasingState;
                    break;
                case EnemyStates.Attacking:
                    enemyTankCurrentState = enemyAttackingState;
                    break;
                default:
                    enemyTankCurrentState = null;
                    break;
            }
            enemyTankCurrentState.OnStateEnter();
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