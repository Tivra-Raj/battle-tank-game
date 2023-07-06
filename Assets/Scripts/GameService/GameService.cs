using BattleTank.EnemyTank;
using BattleTank.PlayerTank;
using BattleTank.UI;
using BattleTank.Utilities;
using System.Collections;
using UnityEngine;

namespace BattleTank.GameService
{
    public class GameService : MonoSingletonGeneric<GameService>
    {
        public TankService Player;
        public EnemyTankService Enemy;

        [SerializeField] private UIView uiService;

        public UIView GetUIService() => uiService;

        private void Update()
        {
            if (Player.TankController.TankView.enabled == false)
            {
                Debug.Log(" tank object is not enabled");
                StartCoroutine(EnemyTankDeath());
            }
        }

        IEnumerator EnemyTankDeath()
        {
            yield return new WaitForSeconds(1f);
            Enemy.EnemyTankController.EnemyTankView.gameObject.SetActive(false);
        }
    }
}