﻿using UnityEngine;

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
        public LayerMask GroundLayerMask;
        public LayerMask PlayerLayerMask;
        public EnemyTankView EnemyTankView;
    }
}