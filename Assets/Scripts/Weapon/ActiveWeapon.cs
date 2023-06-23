using UnityEngine;
using UnityEngine.InputSystem;

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
    public Animator rigController;

    // Start is called before the first frame update
    void Awake()
    {
        playerInput = GetComponent<PlayerInput>();
        shootAction = playerInput.actions["Shoot"];
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
        rigController.Play("equip_" + weapon.weaponName);
        Debug.Log(rigController.GetCurrentAnimatorClipInfo(0).ToString());
        Invoke(nameof(SetAnimationDelayed), 0.001f);
    }

    private void SetAnimationDelayed()
    {
    }

}
