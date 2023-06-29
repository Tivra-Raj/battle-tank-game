using BattleTank.camera;
using BattleTank.EnemyTank;
using System.Collections;
using UnityEngine;

namespace BattleTank.PlayerTank
{
    [RequireComponent (typeof(Rigidbody))]
    public class TankView : MonoBehaviour
    {
        Rigidbody tankRigidbody;
        public TankType tankType;

        private float movementInput;
        private float rotationInput;

        private Vector3 previousPosition;
        [SerializeField] public float totalDistanceTravelled = 0;

        public GameObject explosion;

        public TankController TankController { get; private set; }

        public void SetTankController(TankController tankController) => TankController = tankController;

        private void Awake() => tankRigidbody = GetComponent<Rigidbody>();

        void Start() => previousPosition = this.transform.position;

        void Update()
        {
            Movement();
            DistanceTravelled();
            DistanceTravelledEventTrigger(GetTotalDistanceTravelled());
        }

        void Movement()
        {
            movementInput = Input.GetAxisRaw("Horizontal1");
            rotationInput = Input.GetAxisRaw("Vertical1");

            if (movementInput != 0)
            {
                TankController.Move(movementInput, TankController.TankModel.MovementSpeed);
            }
            else
            {
                tankRigidbody.velocity = Vector3.zero;
            }

            if (rotationInput != 0)
            {
                TankController.Turn(rotationInput, TankController.TankModel.RotationSpeed);
            }
        }

        void DistanceTravelled()
        {
            float distance = Vector3.Distance(this.transform.position, previousPosition);
            totalDistanceTravelled += distance;
            previousPosition = this.transform.position;
        }

        public float GetTotalDistanceTravelled() => totalDistanceTravelled;

        public void DistanceTravelledEventTrigger(float distanceTravelled)
        {
            if (distanceTravelled == 100f || distanceTravelled == 500f || distanceTravelled == 1000f)
            {
                Debug.Log("Distanced travelled = " + distanceTravelled);
                EventService.Instance.OnDistanceTravelledEvent.InvokeEvent(distanceTravelled);
            }
        }

        public Rigidbody GetRigidbody() => tankRigidbody;

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.GetComponent<EnemyTankView>() != null)
            {
                CameraService.Instance.DeathCameraSetup();

                explosion = Instantiate(explosion, this.transform.position, Quaternion.identity);
                StartCoroutine(PlayerTankDeath(2));
               
            }
        }

        private IEnumerator PlayerTankDeath(int seconds)
        {  
            yield return new WaitForSecondsRealtime(seconds);
            this.gameObject.GetComponent<TankView>().enabled = false;
            explosion.gameObject.SetActive(false);
            this.gameObject.SetActive(false);
        }
    }
}