using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPickup : MonoBehaviour
{
    public RaycastShoot weaponPrefab;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        ActiveWeapon activeWeapon = other.GetComponent<ActiveWeapon>();
        if (activeWeapon)
        {
            RaycastShoot weapon = Instantiate(weaponPrefab);
            activeWeapon.Equip(weapon);
        }

    }
}
