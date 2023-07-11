using BattleTank.BulletShooting;
using BattleTank.PlayerTank;
using UnityEngine;
using UnityEngine.AI;

namespace BattleTank.EnemyTank
{
    public class EnemyTankController
    {
        private int currentHealth;
        private float waitTime;
        private float canFire;

        public EnemyTankModel EnemyTankModel { get; private set; }
        public EnemyTankView EnemyTankView { get; private set; }

        public EnemyTankController(EnemyTankModel _enemyTankModel, EnemyTankView _enemyTankView)
        {
            EnemyTankModel = _enemyTankModel;
            EnemyTankView = GameObject.Instantiate<EnemyTankView>(_enemyTankView);

            EnemyTankModel.SetEnemyTankController(this);
            EnemyTankView.SetEnemyTankController(this);

            waitTime = EnemyTankModel.PatrolTime;
        }

        public void Configure(Vector3 setPosition)
        {
           EnemyTankView.transform.position = setPosition;   
        }

        #region EnemyPatrolling
        public void Patroling()
        {
            if (waitTime <= 0)
            {
                SetPatrolingDestination();
                waitTime = EnemyTankModel.PatrolTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }

        private void SetPatrolingDestination()
        {
            Vector3 walkPoint = SearchRandomWalkPoint();
            EnemyTankView.GetNavMeshAgent().SetDestination(walkPoint);
            EnemyTankView.GetNavMeshAgent().stoppingDistance = 0;
        }

        private Vector3 SearchRandomWalkPoint()
        {

            float randomZ = Random.Range(-EnemyTankView.GetWalkPointRange(), EnemyTankView.GetWalkPointRange());
            float randomX = Random.Range(-EnemyTankView.GetWalkPointRange(), EnemyTankView.GetWalkPointRange());

            Vector3 randomDirection = new Vector3(randomX, EnemyTankView.transform.position.y, randomZ);

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 0.1f, NavMesh.AllAreas);
            return hit.position;
        }
        #endregion

        #region EnemyChasing
        public void Chasing()
        {
            EnemyTankView.transform.LookAt(TankService.Instance.TankController.TankView.transform.position);
            EnemyTankView.GetNavMeshAgent().SetDestination(TankService.Instance.TankController.TankView.transform.position);
            EnemyTankView.GetNavMeshAgent().stoppingDistance = EnemyTankModel.attackRange;
        }
        #endregion

        #region EnemyAttacking
        public void Attacking()
        {
            EnemyTankView.transform.LookAt(TankService.Instance.TankController.TankView.transform.position);
            HandleShooting();
        }

        private void HandleShooting()
        {
            if (canFire < Time.time)
            {
                canFire = EnemyTankModel.FireRate + Time.time;
                BulletService.Instance.GetBullet(EnemyTankView.turetTransform.position, EnemyTankView.transform.rotation, EnemyTankModel.BulletType);
            }
        }
        #endregion

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            if (currentHealth <= 0)
                DestroyEnemy();
        }

        public void OnEnemyCollided(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<TankView>() != null)
            {
                TankService.Instance.TankController.TakeDamage(EnemyTankModel.damageToInflict);
                DestroyEnemy();
            }
        }

        private void DestroyEnemy()
        {
            GameService.GameService.Instance.GetUIService().IncrementScore(EnemyTankModel.scoreToGrant);
            EnemyTankView.gameObject.SetActive(false);
            EnemyTankService.Instance.ReturnEnemyToPool(this);
        }
    }
}