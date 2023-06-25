using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank.Enemy_AI_State
{
    public class EnemyPatrolingState : EnemyTankState
    {
        [SerializeField] private Vector3 walkPoint;
        [SerializeField] private bool walkPointSet;

        [SerializeField] private float waitTime;
        [SerializeField] private float startWaitTime;
        public override void OnStateEnter()
        {
            base.OnStateEnter();
            Debug.Log("entering : Patroling state");
            waitTime = startWaitTime;
            Patroling();
        }

        public override void OnStateExit()
        {
            base.OnStateExit();
            Debug.Log("exiting : Patroling state");
        }

        private void Update()
        {
            Patroling();

            playerInSightRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetSightRange(), enemyTankView.GetPlayerLayerMask());
            playerInAttackRange = Physics.CheckSphere(enemyTankView.transform.position, enemyTankView.GetAttackRange(), enemyTankView.GetPlayerLayerMask());

            if (playerInSightRange && !playerInAttackRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyChasingState);
            }
            else if (playerInSightRange && playerInAttackRange)
            {
                enemyTankView.ChangeEnemyState(enemyTankView.enemyAttackingState);
            }
        }

        public void Patroling()
        {
            if (!walkPointSet && waitTime <= 0)
            {
                SearchRandomWalkPoint();
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }

            if (walkPointSet)
            {
                enemyTankView.GetNavMeshAgent().SetDestination(walkPoint);
            }

            //walkpoint reached
            if (enemyTankView.GetNavMeshAgent().remainingDistance <= enemyTankView.GetNavMeshAgent().stoppingDistance)
            {
                walkPointSet = false;
            }

            Debug.Log("walk point set status = " + walkPointSet);
        }

        private Vector3 SearchRandomWalkPoint()
        {

            float randomZ = Random.Range(-enemyTankView.GetWalkPointRange(), enemyTankView.GetWalkPointRange());
            float randomX = Random.Range(-enemyTankView.GetWalkPointRange(), enemyTankView.GetWalkPointRange());

            walkPoint = new Vector3(enemyTankView.transform.position.x + randomX, enemyTankView.transform.position.y, enemyTankView.transform.position.z + randomZ);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(walkPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                walkPointSet = true;
            }
            Debug.Log(" Enemy tank patroling");
            return hit.position;
        }
    }
}