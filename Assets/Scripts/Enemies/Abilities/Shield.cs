using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour, IDamageable
{
    public float maxHealth;
    public float health;



    private void OnEnable()
    {
        Invoke(nameof(ShieldBreak), 8f);
    }

    public void GetDamage(float dmg)
    {
        health -= dmg;
        if (health < 0f)
        {
            ShieldBreak();
        }
    }

    public void GetHeal(float healAmount)
    {
        throw new System.NotImplementedException();
        //Shield cannot heal.
    }

    private void ShieldBreak()
    {
        gameObject.SetActive(false);
    }

}
