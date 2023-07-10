using BattleTank.camera;
using BattleTank.EnemyTank;
using BattleTank.Interface;
using System.Collections;
using UnityEngine;

namespace BattleTank.PlayerTank
{
    [RequireComponent (typeof(Rigidbody))]
    public class TankView : MonoBehaviour, IDamagable
    {
        private Rigidbody tankRigidbody;
        public TankType tankType;

        [SerializeField] public Transform turetTransform;
        public ParticleSystem explosion;

        private Coroutine playerTankDeath;

        public TankController TankController { get; private set; }

        public void SetTankController(TankController tankController) => TankController = tankController;

        private void Awake() => tankRigidbody = GetComponent<Rigidbody>();

        void Update()
        {
            TankController.HadleTankInput();
        }

        public Rigidbody GetRigidbody() => tankRigidbody;

        public void TakeDamage(int damageToTake) => TankController.TakeDamage(damageToTake);

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyTankView>() != null)
            {
                stopCoroutine(playerTankDeath);
                CameraService.Instance.DeathCameraSetup();

                explosion = Instantiate(explosion, this.transform.position, this.transform.rotation);
                
                playerTankDeath =  StartCoroutine(TankController.PlayerTankDeath(2));
               
            }
        }

        private void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}