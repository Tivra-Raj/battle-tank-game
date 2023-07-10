using System;
using UnityEditor;
using UnityEngine;

namespace BattleTank.AchievementSO
{
    [CreateAssetMenu(fileName = "DistancTravelledAchievementSO", menuName = "ScriptableObject/Achievement/NewDistancTravelledAchievementSO")]
    public class DistanceTravelledAchievementSO : ScriptableObject
    {
        public AchievementLevel[] Levels;

        [Serializable]
        public class AchievementLevel
        {
            public enum TravelAchievementLevel
            {
                None,
                FirstStep,
                Wanderer,
                JourneyMan,
            }
            public string AchievementName;
            public string AchievementInfo;
            public TravelAchievementLevel SelectAchievementLevel;
            public int Requirement;
        }
    }
}