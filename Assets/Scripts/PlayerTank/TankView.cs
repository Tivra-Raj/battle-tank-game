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

        private Vector3 previousPosition;
        public float totalDistanceTravelled = 0;

        [SerializeField] public Transform turetTransform;
        public ParticleSystem explosion;

        private Coroutine playerTankDeath;

        public TankController TankController { get; private set; }

        public void SetTankController(TankController tankController) => TankController = tankController;

        private void Awake() => tankRigidbody = GetComponent<Rigidbody>();

        void Start() => previousPosition = this.transform.position;

        void Update()
        {
            TankController.HadleTankInput();
            DistanceTravelled();
            DistanceTravelledEventTrigger(GetTotalDistanceTravelled());
        }

        public Rigidbody GetRigidbody() => tankRigidbody;

        public void TakeDamage(int damageToTake) => TankController.TakeDamage(damageToTake);

        void DistanceTravelled()
        {
            float distance = Vector3.Distance(this.transform.position, previousPosition);
            totalDistanceTravelled += distance;
            previousPosition = this.transform.position;
        }

        public float GetTotalDistanceTravelled() => totalDistanceTravelled;

        public void DistanceTravelledEventTrigger(float distanceTravelled)
        {
            if (distanceTravelled >= 100f || distanceTravelled >= 500f || distanceTravelled >= 1000f)
            {
                Debug.Log("Distanced travelled = " + distanceTravelled);
                EventService.Instance.OnDistanceTravelledEvent.InvokeEvent(100f);
            }
        }

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