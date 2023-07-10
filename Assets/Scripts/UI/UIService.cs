using BattleTank.Utilities;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Threading.Tasks;
using System;

namespace BattleTank.UI
{
    public class UIService : MonoSingletonGeneric<UIService>
    {
        [SerializeField] private TextMeshProUGUI scoreText;
        [SerializeField] private TextMeshProUGUI healthText;
        [SerializeField] private GameObject gameplayPanel;
        [SerializeField] private GameObject gameOverPanel;
        [SerializeField] private Button playAgainButton;
        [SerializeField] private Button quitButton;
        [SerializeField] private AudioSource audioSource;

        [Header("Achievement PopUp")]
        [SerializeField] private GameObject achievementPopup;
        [SerializeField] private TextMeshProUGUI achievementText;
        [SerializeField] private TextMeshProUGUI achievementNameText;
        [SerializeField] private TextMeshProUGUI achievementInfoText;
        
        private int currentScore;

        private void Start()
        {
            currentScore = 0;
            IncrementScore(currentScore);
            achievementPopup.SetActive(false);
        }

        public void IncrementScore(int scoreToIncrement)
        {
            currentScore += scoreToIncrement;
            scoreText.SetText(currentScore.ToString());
        }

        public void UpdateHealthUI(int healthToDisplay) => healthText.SetText(healthToDisplay.ToString());

        public async void ShowAchievementUnlocked(string achievementName, string achievementInfo, float PopUpDisplayDuration)
        {
            achievementPopup.SetActive(true);
            achievementText.text = "ACHIEVEMENT UNLOCKED !";
            achievementNameText.text = achievementName;
            achievementInfoText.text = achievementInfo;
            await Task.Delay(TimeSpan.FromSeconds(PopUpDisplayDuration));
            achievementPopup.SetActive(false);
        }

        public void EnableGameOverUI()
        {
            gameplayPanel.SetActive(false);
            audioSource.enabled = false;
            gameOverPanel.SetActive(true);
            playAgainButton.onClick.AddListener(OnPlayAgainClicked);
            quitButton.onClick.AddListener(OnQuitClicked);
        }

        private void OnPlayAgainClicked() => SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);

        private void OnQuitClicked() => Application.Quit();
    }
}