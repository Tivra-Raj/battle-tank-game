using UnityEngine;

namespace BattleTank.BulletShooting
{
    public class BulletView : MonoBehaviour
    {
        private Rigidbody bulletRigidbody;
        public BulletController BulletController { get; private set; }

        public void SetBulletController(BulletController bulletController)
        {
            BulletController = bulletController;
            bulletRigidbody = GetComponent<Rigidbody>();
        }

        private void Update() => BulletController?.ShootBullet();

        private void OnTriggerEnter2D(Collider2D collision) => BulletController?.OnBulletEnteredTrigger(collision.gameObject);

        public Rigidbody GetBulletRigidbody() => bulletRigidbody;

    }
}