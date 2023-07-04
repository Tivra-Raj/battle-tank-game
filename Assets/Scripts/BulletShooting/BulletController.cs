using BattleTank.Interface;
using BattleTank.PlayerTank;
using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletController : IBullet
    {
        private Transform spawnTransform;

        public BulletModel BulletModel { get; private set; }
        public BulletView BulletView { get; private set; }

        public BulletController(BulletModel _bulletModel, BulletView _bulletView)
        {
            BulletModel = _bulletModel;
            BulletView = GameObject.Instantiate<BulletView>(_bulletView);

            BulletModel.SetBulletController(this);
            BulletView.SetBulletController(this);
        }

        public void ConfigureBullet(Transform spawnTransform)
        {
            BulletView.gameObject.SetActive(true);
            this.spawnTransform = spawnTransform;
            BulletView.transform.position = spawnTransform.position;
            BulletView.transform.rotation = spawnTransform.rotation;
        }

        public void ShootBullet()
        {
            BulletView.GetBulletRigidbody().AddForce(spawnTransform.forward * BulletModel.speed, ForceMode.Impulse);
        }

        public void OnBulletEnteredTrigger(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<IDamagable>() != null)
            {
                collidedGameObject.GetComponent<IDamagable>().TakeDamage(BulletModel.damage);
                BulletView.gameObject.SetActive(false);
                TankService.Instance.ReturnBulletToPool(this);
            }
        }
    }
}