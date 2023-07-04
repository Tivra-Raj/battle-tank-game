using BattleTank.BulletShooting;
using BattleTank.GameService;
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
        private BulletPool bulletPool;

        private ShootingState currentShootingState;
        private int currentHealth;

        public TankModel TankModel { get; private set; }
        public TankView TankView { get; private set; }

        public TankController(TankModel _tankmodel, TankView _tankview, BulletPool bulletPool)
        {
            TankModel = _tankmodel;
            TankView = GameObject.Instantiate<TankView>(_tankview);
            tankRigidbody = TankView.GetRigidbody();

            TankModel.SetTankController(this);
            TankView.SetTankController(this);
            this.bulletPool = bulletPool;

            InitializeVariables();
        }

        private void InitializeVariables()
        {
            currentHealth = (int)TankModel.Health;
            currentShootingState = ShootingState.NotFiring;
            GameService.GameService.Instance.GetUIService().UpdateHealthUI(currentHealth);
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

        private void HandleShooting()
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
                FireBullet();
            if (Input.GetKeyUp(KeyCode.Mouse0))
                currentShootingState = ShootingState.NotFiring;
        }

        private void FireBullet()
        {
            currentShootingState = ShootingState.Firing;
            FireBulletAtPosition(TankView.turetTransform);
        }

        private void FireBulletAtPosition(Transform fireLocation)
        {
            //BulletController bulletToFire = bulletPool.GetBullet();
            //bulletToFire.ConfigureBullet(fireLocation);
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
            yield return new WaitForSecondsRealtime(seconds);
            Object.Destroy(TankView.gameObject);
            TankView.explosion.gameObject.SetActive(false);
            currentShootingState = ShootingState.NotFiring;
            GameService.GameService.Instance.GetUIService().EnableGameOverUI();
        }

        private enum ShootingState
        {
            Firing,
            NotFiring
        }
    }
}