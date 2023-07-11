using BattleTank.BulletShooting;
using UnityEngine;

namespace BattleTank.EnemyTank
{
    public class EnemyTankModel
    {
        public float EnemyHealth;
        public float WalkPointRange;
        public float sightRange;
        public float attackRange;
        public int damageToInflict;
        public int scoreToGrant;
        public int PatrolTime;
        public int FireRate;
        public LayerMask GroundLayerMask;
        public LayerMask PlayerLayerMask;
        public EnemyTankType EnemyTankType;
        public BulletScriptableObject BulletType;

        public EnemyTankController EnemyTankController { get; private set; }

        public void SetEnemyTankController(EnemyTankController enemyTankController)
        {
            EnemyTankController = enemyTankController;
        }

        public EnemyTankModel(EnemyTankScriptableObject enemyTankScriptableObject)
        {
            EnemyTankType = enemyTankScriptableObject.EnemyTankType;
            EnemyHealth = enemyTankScriptableObject.EnemyHealth;
            WalkPointRange = enemyTankScriptableObject.WalkPointRange;
            sightRange = enemyTankScriptableObject.sightRange;
            attackRange = enemyTankScriptableObject.attackRange;
            damageToInflict = enemyTankScriptableObject.damageToInflict;
            scoreToGrant = enemyTankScriptableObject.scoreToGrant;
            PatrolTime = enemyTankScriptableObject .PatrolTime;
            FireRate = enemyTankScriptableObject .FireRate;
            GroundLayerMask = enemyTankScriptableObject.GroundLayerMask;
            PlayerLayerMask = enemyTankScriptableObject.PlayerLayerMask;
            BulletType = enemyTankScriptableObject .BulletType;
        }
    }
}