using BattleTank.EnemyTank;
using BattleTank.PlayerTank;
using System.Collections;
using UnityEngine;

namespace BattleTank.GameManager
{
    public class GameManager : MonoSingletonGeneric<GameManager>
    {
        public GameObject GameOver;
        public GameObject Audio;
        public TankService Player;
        public EnemyTankService Enemy;

        private void Start()
        {
            GameOver.SetActive(false);
        }

        private void Update()
        {
            if (Player.TankController.TankView.enabled == false)
            {
                Debug.Log(" tank object is not enabled");
                StartCoroutine(EnemyAnime());
            }
        }

        IEnumerator EnemyAnime()
        {
            yield return new WaitForSeconds(1f);
            Enemy.EnemyTankController.EnemyTankView.gameObject.SetActive(false);
            DeathText();
        }

        public void DeathText()
        {
            GameOver.SetActive(true);
            Audio.SetActive(false);
        }
    }
}