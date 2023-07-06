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

        public BulletController(BulletModel _bulletModel, BulletView _bulletView, Vector3 bulletPosition, Quaternion bulletRotation)
        {
            BulletModel = _bulletModel;
            BulletView = GameObject.Instantiate<BulletView>(_bulletView, bulletPosition, bulletRotation);

            BulletModel.SetBulletController(this);
            BulletView.SetBulletController(this);

            BulletView.gameObject.SetActive(true);
        }

        public void ShootBullet()
        {
            Vector3 shoot = BulletView.transform.position += BulletView.transform.forward * BulletModel.speed * Time.fixedDeltaTime;
            BulletView.GetBulletRigidbody().MovePosition(shoot);
        }

        public void OnBulletEnteredTrigger(GameObject collidedGameObject)
        {
            if (collidedGameObject.GetComponent<IDamagable>() != null)
            {
                collidedGameObject.GetComponent<IDamagable>().TakeDamage(BulletModel.damage);
                BulletView.gameObject.SetActive(false);
                TankService.Instance.ReturnBulletToPool(this);
            }
            else
            {
                BulletView.gameObject.SetActive(false);
                TankService.Instance.ReturnBulletToPool(this);
            }
        }
    }
}