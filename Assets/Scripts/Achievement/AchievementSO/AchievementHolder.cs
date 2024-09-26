using UnityEngine;

namespace BattleTank.AchievementSO
{
    [CreateAssetMenu(fileName = "AchievementHolder", menuName = "ScriptableObject/Achievement/NewAchievementListSO")]
    public class AchievementHolder : ScriptableObject
    {
        public BulletsFiredAchievementSO BulletFiredAchievementSO;
        public EnemiesKilledAchievementSO EnemiesKilledAchievementSO;
        public DistanceTravelledAchievementSO DistanceTravelledAchievementSO;
    }
}