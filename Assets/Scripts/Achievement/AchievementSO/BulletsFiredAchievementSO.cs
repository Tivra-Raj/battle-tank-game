using System;
using UnityEngine;

namespace BattleTank.AchievementSO
{
    [CreateAssetMenu(fileName = "BulletsFiredAchievementSO", menuName = "ScriptableObject/Achievement/NewBulletsFiredAchievementSO")]
    public class BulletsFiredAchievementSO : ScriptableObject
    {
        public AchievementLevel[] Levels;

        [Serializable]
        public class AchievementLevel
        {
            public enum BulletAchievementLevel
            {
                None,
                BulletTamer,
                BullerBallet,
                TheMarksman,
            }
            public string AchievementName;
            public string AchievementInfo;
            public BulletAchievementLevel SelectAchievementLevel;
            public int Requirement;
        }
    }
}