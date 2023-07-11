using BattleTank.Achievement;
using BattleTank.BulletShooting;
using System.Collections;
using UnityEngine;

namespace BattleTank.PlayerTank
{
    [System.Serializable]
    public class TankController
    {
        private Rigidbody tankRigidbody;

        private float movementInput;
        private float rotationInput;

        private int currentHealth;

        private Vector3 previousPosition;

        private float canFire;

        public TankModel TankModel { get; private set; }
        public TankView TankView { get; private set; }

        public TankController(TankModel _tankmodel, TankView _tankview)
        {
            TankModel = _tankmodel;
            TankView = GameObject.Instantiate<TankView>(_tankview);
            tankRigidbody = TankView.GetRigidbody();
            previousPosition = TankView.transform.position;

            TankModel.SetTankController(this);
            TankView.SetTankController(this);

            SubscribeEvents();

            currentHealth = (int)TankModel.Health;
            GameService.GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
        }

        private void SubscribeEvents()
        {
            EventService.Instance.OnPlayerFiredBulletEvent.AddListener(UpdateBulletsFiredCounter);
            EventService.Instance.OnPlayerTravelledDistanceEvent.AddListener(UpdateDistanceTravelledCounter);
        }

        private void UnSubscribeEvents()
        {
            EventService.Instance.OnPlayerFiredBulletEvent.RemoveListener(UpdateBulletsFiredCounter);
            EventService.Instance.OnPlayerTravelledDistanceEvent.RemoveListener(UpdateDistanceTravelledCounter);
        }

        public void HadleTankInput()
        {
            HandleTankMovement();
            HandleShooting();
        }

        #region Tank Movement
        private void HandleTankMovement()
        {
            movementInput = Input.GetAxisRaw("Horizontal1");
            rotationInput = Input.GetAxisRaw("Vertical1");

            if (movementInput != 0)
            {
                Move(movementInput, TankModel.MovementSpeed);
            }
            else
            {
                tankRigidbody.velocity = Vector3.zero;
            }

            if (rotationInput != 0)
            {
                Turn(rotationInput, TankModel.RotationSpeed);
            }

            EventService.Instance.OnPlayerTravelledDistanceEvent.InvokeEvent();
        }

        public void Move(float movementInput, float movementSpeed)
        {
            tankRigidbody.velocity = TankView.transform.forward * movementInput * movementSpeed * Time.deltaTime;
        }

        public void Turn(float rotationInput, float rotationSpeed)
        {
            float turn = rotationInput * rotationSpeed * Time.deltaTime;
            Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
            tankRigidbody.MoveRotation(tankRigidbody.rotation * turnRotation);
        }
        #endregion

        #region Tank Shooting
        private void HandleShooting()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if(canFire < Time.time)
                {
                    canFire = TankModel.FireRate + Time.time;
                    FireBullet();
                }
            }
        }

        private void FireBullet()
        {
            BulletService.Instance.GetBullet(TankView.turetTransform.position, TankView.transform.rotation, TankModel.BulletType);
            EventService.Instance.OnPlayerFiredBulletEvent.InvokeEvent();
        }

        private void UpdateBulletsFiredCounter()
        {
            TankModel.BulletsFired += 1;
            AchievementService.Instance.GetAchievementController().CheckForBulletFiredAchievement();
        }
        #endregion

        public void UpdateDistanceTravelledCounter()
        {
            float distance = Vector3.Distance(TankView.transform.position, previousPosition);
            TankModel.DistanceTravelled += distance;
            AchievementService.Instance.GetAchievementController().CheckForDistanceTravelledAchievement();
            previousPosition = TankView.transform.position;
        }

        public void TakeDamage(int damageToTake)
        {
            currentHealth -= damageToTake;
            GameService.GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);

            if (currentHealth <= 0)
                 PlayerTankDeath(2);
        }

        public IEnumerator PlayerTankDeath(int seconds)
        {
            yield return new WaitForSeconds(seconds);
            Object.Destroy(TankView.gameObject);
            TankView.explosion.gameObject.SetActive(false);
            UnSubscribeEvents();
            GameService.GameService.Instance.GetUIService().EnableGameOverUI();
        }
    }
}