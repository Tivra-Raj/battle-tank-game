﻿using UnityEngine;

namespace BattleTank.BulletShooting
{
    public interface IBullet
    {
        public void ShootBullet();
        public void OnBulletEntered(GameObject collidedObject);
    }
}