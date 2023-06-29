using System.Collections;
using UnityEngine;
using TMPro;

namespace BattleTank.Achievement
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private AchievementSciprtableObject achievementInstruction;

        [Header("Instruction Popup")]
        [SerializeField] private GameObject achievementPopup;
        [SerializeField] private TextMeshProUGUI achievementText;

        private Coroutine achievementCoroutine;

        private void Start()
        {
            HideInstructionPopup();
            EventService.Instance.OnDistanceTravelledEvent.AddListener(ShowDistanceTravelledInstructions);
        }
        private void OnDestroy()
        {
            EventService.Instance.OnDistanceTravelledEvent.RemoveListener(ShowDistanceTravelledInstructions);
        }

        private void HideInstructionPopup()
        {
            achievementText.SetText(string.Empty);
            achievementPopup.SetActive(false);
            stopCoroutine(achievementCoroutine);
        }

        private void ShowInstructionPopup(float distanceTravelled)
        {
            achievementText.SetText(achievementInstruction.Instruction + distanceTravelled);
            achievementPopup.SetActive(true);
        }

        private void ShowDistanceTravelledInstructions(float distanceTravelled)
        {
            stopCoroutine(achievementCoroutine);
            switch (distanceTravelled)
            {
                case 100f:
                    ShowInstructionPopup(distanceTravelled);
                    break;
                case 500f:
                    ShowInstructionPopup(distanceTravelled);
                    break;
                case 1000f:
                    ShowInstructionPopup(distanceTravelled);
                    break;
            }
            achievementCoroutine = StartCoroutine(setInstructionsTimer(distanceTravelled));
        }

        private IEnumerator setInstructionsTimer(float distanceTravelled)
        {
            yield return new WaitForSeconds(achievementInstruction.WaitToTriggerDuration);
            ShowInstructionPopup(distanceTravelled);

            yield return new WaitForSeconds(achievementInstruction.DisplayDuration);
            HideInstructionPopup();
        }

        private void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                coroutine = null;
                StopCoroutine(coroutine);
            }
        }
    }
}