using BattleTank.BulletShooting;
using UnityEngine;

namespace BattleTank.EnemyTank
{
    [CreateAssetMenu (fileName = "EnemyTankScriptableObject", menuName = "ScriptableObject/CreateNewEnemyTankScriptableObject")]
    public class EnemyTankScriptableObject : ScriptableObject
    {
        public EnemyTankType EnemyTankType;
        public string EnemyTankName;
        public float WalkPointRange;
        public int EnemyHealth;
        public float sightRange;
        public float attackRange;
        public int damageToInflict;
        public int scoreToGrant;
        public int PatrolWaitTime;
        public int FireRate;
        public LayerMask PlayerLayerMask;
        public BulletScriptableObject BulletType;
        public EnemyTankView EnemyTankView;
    }
}