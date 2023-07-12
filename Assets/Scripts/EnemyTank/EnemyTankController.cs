using BattleTank.Achievement;
using BattleTank.BulletShooting;
using BattleTank.PlayerTank;
using BattleTank.UI;
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

            waitTime = EnemyTankModel.PatrolWaitTime;
            currentHealth = (int)EnemyTankModel.EnemyHealth;

            SubscribeEvents();
        }

        public void Configure(Vector3 setPosition)
        {
           EnemyTankView.transform.position = setPosition;   
        }

        private void SubscribeEvents()
        {
            EventService.Instance.OnPlayerKilledEnemiesEvent.AddListener(UpdateEnemiesKilledCounter);
        }

        private void UnSubscribeEvents()
        {
            EventService.Instance.OnPlayerKilledEnemiesEvent.RemoveListener(UpdateEnemiesKilledCounter);
        }

        #region EnemyPatrolling
        public void Patroling()
        {
            if (waitTime <= 0)
            {
                SetPatrolingDestination();
                waitTime = EnemyTankModel.PatrolWaitTime;
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

            float randomZ = Random.Range(-EnemyTankModel.WalkPointRange, EnemyTankModel.WalkPointRange);
            float randomX = Random.Range(-EnemyTankModel.WalkPointRange, EnemyTankModel.WalkPointRange);

            Vector3 randomDirection = new Vector3(EnemyTankView.transform.position.x + randomX, EnemyTankView.transform.position.y, EnemyTankView.transform.position.z + randomZ);

            NavMeshHit hit;
            NavMesh.SamplePosition(randomDirection, out hit, 0.1f, NavMesh.AllAreas);
            return hit.position;
        }
        #endregion

        #region EnemyChasing
        public void Chasing()
        {
            EnemyTankView.GetNavMeshAgent().SetDestination(TankService.Instance.TankController.TankView.transform.position);
            EnemyTankView.GetNavMeshAgent().stoppingDistance = EnemyTankModel.attackRange;
        }
        #endregion

        #region EnemyAttacking
        public void Attacking()
        {
            EnemyTankView.transform.LookAt(TankService.Instance.TankController.TankView.transform);
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

        private void UpdateEnemiesKilledCounter()
        {
            TankService.Instance.TankController.TankModel.EnemiesKilled += 1;
            AchievementService.Instance.GetAchievementController().CheckForEnemiesKilledAchievement();
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;

            if (currentHealth <= 0)
                DestroyEnemy();
        }

        private void DestroyEnemy()
        {
            GameService.GameService.Instance.GetUIService().IncrementScore(EnemyTankModel.scoreToGrant);
            EnemyTankView.gameObject.SetActive(false);
            EventService.Instance.OnPlayerKilledEnemiesEvent.InvokeEvent();
            EnemyTankService.Instance.ReturnEnemyToPool(this);
            UnSubscribeEvents();
        }
    }
}