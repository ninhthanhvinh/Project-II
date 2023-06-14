using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Abilities
{
    public GameObject VFX;
    public float buffTime;

    private void Start()
    {
        boss = GetComponent<Boss>();
        anim = GetComponent<Animator>();
        GetUse();
    }


    public override void GetUse()
    {
        if (boss.mana <= manaConsumed)
        {
            return;
        }
        anim.SetTrigger("powerUp");
        Instantiate(VFX, transform.position, Quaternion.identity);
        boss.speed *= 2;
        boss.mana -= manaConsumed;
        //Buff HP, Armor, ...
        Invoke(nameof(Debuff), buffTime);

    }

    void Debuff()
    {
        boss.speed /= 2;
        //Debuff HP, Armor, ...

    }
}
