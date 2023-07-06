using UnityEngine;

namespace BattleTank.BulletShooting
{
    public interface IBullet
    {
        public void ShootBullet();
        public void OnBulletEnteredTrigger(GameObject collidedObject);
    }
}