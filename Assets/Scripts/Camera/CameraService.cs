using BattleTank.EnemyTank;
using BattleTank.PlayerTank;
using UnityEngine;

namespace BattleTank.camera
{
    public class CameraService : MonoSingletonGeneric<CameraService>
    {
        public Camera cam;
        public Transform deathCameraPosition;
        public float smoothSpeed;
        public float speed;

        private Vector3 cameraPosition;

        private void Start()
        {
            CameraSetup();
        }

        private void FixedUpdate()
        {
            Vector3 a = cam.transform.position;
            Vector3 b = deathCameraPosition.position;
            cameraPosition = Vector3.MoveTowards(a, Vector3.Lerp(a, b, smoothSpeed), speed);
        }

        public void CameraSetup()
        {
            cam.transform.SetParent(TankService.Instance.TankController.TankView.transform);
            cam.transform.position = new Vector3(0f, 8.1f, -13f);
            cam.transform.eulerAngles = new Vector3(15f, 0f, 0f);
        }

        public void DeathCameraSetup()
        {
            cam.transform.SetParent(null);
            cam.transform.position = new Vector3(-48, 28, -51);
            cam.transform.eulerAngles = new Vector3(35, 41, 0);
            
        }
    }
}