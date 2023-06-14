using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEditor.Animations;

public class ActiveWeapon : MonoBehaviour
{
    public Transform crosshair;
    public Transform target;
    public UnityEngine.Animations.Rigging.Rig handIK;
    public Transform weaponParent;
    public int bulletsTotal;

    public Transform weaponLeftGrip;
    public Transform weaponRightGrip;

    private PlayerInput playerInput;
    private InputAction shootAction;

    private bool isFiring = false;
    private float accumulatedTime;
    private float interval;
    private int currentBulletsInLoad;

    RaycastShoot weapon;
    Animator anim;
    AnimatorOverrideController overrideController;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
        anim = GetComponent<Animator>();
        overrideController = anim.runtimeAnimatorController as AnimatorOverrideController;

    }

    private void Start()
    {
        RaycastShoot existingWeapon = GetComponentInChildren<RaycastShoot>();
        if (existingWeapon != null)
        {
            Equip(existingWeapon);
        }
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


    // Update is called once per frame
    void Update()
    {
        if (weapon)
        {
            if (isFiring && currentBulletsInLoad > 0)
            {
                while (accumulatedTime <= 0f)
                {
                    weapon.FireShoot();
                    accumulatedTime = interval;
                    currentBulletsInLoad--;
                    Debug.Log(currentBulletsInLoad);
                }
                accumulatedTime -= Time.deltaTime;
            }
            if (currentBulletsInLoad <= 0)
            {
                //Reload'
                Debug.Log("REload");
                currentBulletsInLoad = bulletsTotal;

            }
        }
        else
        {
            handIK.weight = 0f;
            anim.SetLayerWeight(1, 0.0f);
        }
    }

    private static void Reload()
    {
        Debug.Log("Reload");
    }

    public void Equip(RaycastShoot newWeapon)
    {
        if (weapon)
            Destroy(weapon.gameObject);
        weapon = newWeapon;
        interval = 1 / weapon.fireRate;
        currentBulletsInLoad = weapon.maxBulletsInLoad;
        weapon.crosshair = crosshair;
        weapon.target = target;
        weapon.transform.parent = weaponParent;
        weapon.transform.localPosition = Vector3.zero;
        weapon.transform.localRotation = Quaternion.identity;
        handIK.weight = 1.0f;
        anim.SetLayerWeight(1, 1.0f);
        Invoke(nameof(SetAnimationDelayed), 0.001f);
    }

    private void SetAnimationDelayed()
    {
        overrideController["Empty_Hand_Animation"] = weapon.weaponAnimation;
    }

    [ContextMenu("Save Weapon Pose")]
    public void SaveWeaponPose()
    {
        GameObjectRecorder gameObjectRecorder = new(gameObject);
        gameObjectRecorder.BindComponentsOfType<Transform>(weaponParent.gameObject, false);
        gameObjectRecorder.BindComponentsOfType<Transform>(weaponLeftGrip.gameObject, false);
        gameObjectRecorder.BindComponentsOfType<Transform>(weaponRightGrip.gameObject, false);
        gameObjectRecorder.TakeSnapshot(0.0f);
        gameObjectRecorder.SaveToClip(weapon.weaponAnimation);
    }
}
