using BattleTank.AchievementSO;

namespace BattleTank.Achievement
{
    public class AchievementModel
    {
        public BulletsFiredAchievementSO BulletsFiredAchievementSO { get; private set; }
        public EnemiesKilledAchievementSO EnemiesKilledAchievementSO { get; private set; }
        public DistanceTravelledAchievementSO DistanceTravelledAchievementSO { get; private set; }

        public AchievementModel(AchievementHolder achievements)
        {
            BulletsFiredAchievementSO = achievements.BulletFiredAchievementSO;
            EnemiesKilledAchievementSO = achievements.EnemiesKilledAchievementSO;
            DistanceTravelledAchievementSO = achievements.DistanceTravelledAchievementSO;
        }
    }
}