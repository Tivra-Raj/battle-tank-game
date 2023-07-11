using System;
using UnityEngine;

namespace BattleTank.AchievementSO
{
    [CreateAssetMenu(fileName = "EnemiesKilledAchievementSO", menuName = "ScriptableObject/Achievement/NewEnemiesKilledAchievementSO")]
    public class EnemiesKilledAchievementSO : ScriptableObject
    {
        public AchievementLevel[] Levels;

        [Serializable]
        public class AchievementLevel
        {
            public enum EnemiesAchievementLevel
            {
                None,
                Killer,
                Terminator,
                Dominator,
            }
            public string AchievementName;
            public string AchievementInfo;
            public EnemiesAchievementLevel SelectAchievementLevel;
            public int Requirement;
        }
    }
}