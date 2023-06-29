using UnityEngine;

namespace BattleTank.Achievement
{
    [CreateAssetMenu(fileName = "Achievement Sciprtable Object", menuName = "ScriptableObjects/Achievement Sciprtable Object", order = 2)]

    public partial class AchievementSciprtableObject : ScriptableObject
    {
        public string Instruction;
        public int DisplayDuration;
        public int WaitToTriggerDuration = 0;
    }
}