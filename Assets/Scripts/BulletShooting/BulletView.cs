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

        private void FixedUpdate() => BulletController?.ShootBullet();

        private void OnCollisionEnter(Collision collision) => BulletController?.OnBulletEntered(collision.gameObject);

        public Rigidbody GetBulletRigidbody() => bulletRigidbody;
    }
}