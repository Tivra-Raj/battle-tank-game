using BattleTank.AchievementSO;
using BattleTank.Utilities;
using UnityEngine;

namespace BattleTank.Achievement
{
    public class AchievementService : MonoSingletonGeneric<AchievementService>
    {
        [SerializeField] private AchievementHolder AllAchievements;
        private AchievementController achievementController;

        private void Start()
        {
            CreateAchievement();
        }

        private void CreateAchievement()
        {
            AchievementModel model = new AchievementModel(AllAchievements);
            achievementController = new AchievementController(model);
        }

        public AchievementController GetAchievementController()
        {
            return achievementController;
        }
    }
}