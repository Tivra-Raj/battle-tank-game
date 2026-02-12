using BattleTank.PlayerTank;
using BattleTank.UI;

namespace BattleTank.Achievement
{
    public class AchievementController
    {
        public AchievementModel AchievementModel { get; private set; }
        private int currentBulletsFiredAchievementLevel;
        private int currentEnemiesKilledAchievementLevel;
        private int currentDistanceTravelledAchievementLevel;

        public AchievementController(AchievementModel achievementModel)
        {
            currentBulletsFiredAchievementLevel = 0;
            currentEnemiesKilledAchievementLevel = 0;
            currentDistanceTravelledAchievementLevel = 0;
            AchievementModel = achievementModel;
        }

        public void CheckForBulletFiredAchievement()
        {
            for (int i = 0; i < AchievementModel.BulletsFiredAchievementSO.Levels.Length; i++)
            {
                if (i != currentBulletsFiredAchievementLevel) continue;
                if (TankService.Instance.GetTankController().TankModel.BulletsFired == AchievementModel.BulletsFiredAchievementSO.Levels[i].Requirement)
                {
                    UnlockAchievement(AchievementModel.BulletsFiredAchievementSO.Levels[i].AchievementName, AchievementModel.BulletsFiredAchievementSO.Levels[i].AchievementInfo);
                    currentBulletsFiredAchievementLevel = i + 1;
                }
                break;
            }
        }

        public void CheckForEnemiesKilledAchievement()
        {
            for (int i = 0; i < AchievementModel.EnemiesKilledAchievementSO.Levels.Length; i++)
            {
                if (i != currentEnemiesKilledAchievementLevel) continue;
                if (TankService.Instance.GetTankController().TankModel.EnemiesKilled == AchievementModel.EnemiesKilledAchievementSO.Levels[i].Requirement)
                {
                    UnlockAchievement(AchievementModel.EnemiesKilledAchievementSO.Levels[i].AchievementName, AchievementModel.EnemiesKilledAchievementSO.Levels[i].AchievementInfo);
                    currentEnemiesKilledAchievementLevel = i + 1;
                }
                break;
            }
        }

        public void CheckForDistanceTravelledAchievement()
        {
            for (int i = 0; i < AchievementModel.DistanceTravelledAchievementSO.Levels.Length; i++)
            {
                if (i != currentDistanceTravelledAchievementLevel) continue;
                if (TankService.Instance.GetTankController().TankModel.DistanceTravelled >= AchievementModel.DistanceTravelledAchievementSO.Levels[i].Requirement)
                {
                    UnlockAchievement(AchievementModel.DistanceTravelledAchievementSO.Levels[i].AchievementName, AchievementModel.DistanceTravelledAchievementSO.Levels[i].AchievementInfo);
                    currentDistanceTravelledAchievementLevel = i + 1;
                }
                break;
            }
        }

        private void UnlockAchievement(string achievementName, string achievementInfo)
        {
            UIService.Instance.ShowAchievementUnlocked(achievementName, achievementInfo, 3f);
        }
    }
}