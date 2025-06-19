using Cinemachine;
using StarterAssets;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;

public class FPSShooterController : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera aimCamera;
    [SerializeField] private CinemachineVirtualCamera normalCamera; 
    [SerializeField] private float normalSensitivity;
    [SerializeField] private float aimSensitivity;
    [SerializeField] private GameObject aimCrosshair;
    [SerializeField] private LayerMask aimColliderLayerMask = new();
    [SerializeField] private float rayCastDistance = 999f; 
    [SerializeField] private Transform debugTransform;
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform bulletSpawnPoint;


    private StarterAssetsInputs starterAssetsInputs;
    private ThirdPersonController thirdPersonController;
    private bool isAiming = false;
    private Vector3 aimTargetPosition;




    private void Awake()
    {
        starterAssetsInputs = GetComponent<StarterAssetsInputs>();
        thirdPersonController = GetComponent<ThirdPersonController>();

        if (starterAssetsInputs == null)
        {
            Debug.LogError("StarterAssetsInputs component not found on the GameObject.");
        }
        if (aimCamera == null)
        {
            Debug.LogError("Aim Camera is not assigned in the inspector.");
        }
        if (thirdPersonController == null)
        {
            Debug.LogError("ThirdPersonController component not found on the GameObject.");
        }
        if (aimCrosshair == null)
        {
            Debug.LogError("Aim Crosshair is not assigned in the inspector.");
        }
        if (bulletPrefab == null)
        {
            Debug.LogError("Bullet Prefab is not assigned in the inspector.");
        }
        if (bulletSpawnPoint == null)
        {
            Debug.LogError("Bullet Spawn Point is not assigned in the inspector.");
        }
    }
    private void Update()
    {
        // Initialize aimTargetPosition
        aimTargetPosition = Vector3.zero;

        HandleAimToggle();

        // Update the aim camera's priority based on whether the player is aiming
        aimCamera.Priority = isAiming ? 11 : 9;

        AimMode();

    }  
    private void HandleAimToggle()
    {
        if (starterAssetsInputs.aim)
        {
            // Toggle aiming state when the aim input is pressed
            isAiming = !isAiming;            
            starterAssetsInputs.aim = false; 

            // Enable or disable the aim camera and crosshair based on the aiming state
            aimCrosshair.SetActive(isAiming); 
            aimCamera.gameObject.SetActive(isAiming); 

            // Set the sensitivity based on whether the player is aiming or not
            float sensitivity = isAiming ? aimSensitivity : normalSensitivity; 
            thirdPersonController.SetSensitivity(sensitivity); 
        }
    }
    private void AimMode()
    {
        if (isAiming)
        {
            Vector2 crossHairPoint = new Vector2(Screen.width / 2f, Screen.height / 2f);
            Ray ray = Camera.main.ScreenPointToRay(crossHairPoint);

            if (Physics.Raycast(ray, out RaycastHit rayCastHit, rayCastDistance, aimColliderLayerMask))
            {
                // Debug where the camera is aiming
                debugTransform.position = rayCastHit.point;
                aimTargetPosition = rayCastHit.point;
            }

            Vector3 worldAimTarget = aimTargetPosition;
            worldAimTarget.y = transform.position.y;

            Vector3 aimDirection = (worldAimTarget - transform.position).normalized;
            transform.forward = Vector3.Lerp(transform.forward, aimDirection, Time.deltaTime * 10f);

            Debug.DrawRay(ray.origin, ray.direction * 100f, Color.red);
            
            if (starterAssetsInputs.shoot)
            {
                Shoot(aimTargetPosition);
                starterAssetsInputs.shoot = false; 
            }
        }
    }
    private void Shoot(Vector3 targetPosition)
    {
        Debug.Log("Shooting" + bulletPrefab.name);
        Vector3 shootDirection = (targetPosition - bulletSpawnPoint.position).normalized;
        Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.LookRotation(shootDirection, Vector3.up));    
        
    }
}