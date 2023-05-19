using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class RaycastShoot : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem[] muzzleFlashs;
    [SerializeField]
    private ParticleSystem hitEffect;
    [SerializeField]
    private TrailRenderer tracerEffect;
    [SerializeField]
    private Transform target;
    [SerializeField]
    private Transform raycastOrigin;
    [SerializeField]
    private Transform crosshair;
    [SerializeField]
    private float fireRate;

    private PlayerInput playerInput;
    private InputAction shootAction;
    private Ray ray;
    private RaycastHit hitInfo;
    private bool isFiring = false;
    private float accumulatedTime;
    private float interval;
    // Start is called before the first frame update
    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        interval = 1 / fireRate;
        accumulatedTime = interval;
    }

    private void OnEnable()
    {
        shootAction.performed += _ => StartFiring();
        shootAction.canceled += _ => StopFiring();
    }

    private void OnDisable()
    {
        shootAction.performed -= _ => StartFiring();
        shootAction.canceled -= _ => StopFiring();
    }

    private void StartFiring()
    {
        isFiring = true;
    }

    private void StopFiring()
    {
        isFiring = false;
    }



    private void GunShoot()
    {
        //while (accumulatedTime <= 0f)
        //{
        //    //FireShoot();
        //    Debug.Log("Shoot");
        //    accumulatedTime = interval;
        //}
        //accumulatedTime -= Time.deltaTime;
        Debug.Log(isFiring);
    }

    private void FireShoot()
    {
        foreach (var muzzleFlash in muzzleFlashs)
        {
            muzzleFlash.Emit(1);
        }

        ray.origin = raycastOrigin.position;

        var tracer = Instantiate(tracerEffect, ray.origin, Quaternion.identity);
        tracer.AddPosition(ray.origin);

        if (target.position != Vector3.positiveInfinity)
        {
            ray.direction = target.position - raycastOrigin.position;


            if (Physics.Raycast(ray, out hitInfo))
            {
                hitEffect.transform.position = hitInfo.point;
                hitEffect.transform.forward = hitInfo.normal;
                hitEffect.Emit(1);

                tracer.transform.position = hitInfo.point;
            }

            else tracer.transform.position = crosshair.position;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(isFiring)
        {
            while(accumulatedTime <= 0f)
            {
                FireShoot();
                accumulatedTime = interval;
            }
            accumulatedTime -= Time.deltaTime;
        }
    }
}
