using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : Abilities
{
    public GameObject VFX;
    public float buffTime;

    protected float timer;
    protected bool canUse;
    private void Start()
    {
        boss = GetComponent<Boss>();
        anim = GetComponent<Animator>();
        GetUse();
    }


    public override void GetUse()
    {
        if (boss.mana <= manaConsumed || !canUse)
        {
            return;
        }
        anim.SetTrigger("powerUp");
        Instantiate(VFX, transform.position, Quaternion.identity);
        boss.speed *= 2;
        boss.mana -= manaConsumed;
        //Buff HP, Armor, ...
        Invoke(nameof(Debuff), buffTime);
        canUse = false;
        timer = CD;


    }

    void Debuff()
    {
        boss.speed /= 2;
        //Debuff HP, Armor, ...

    }

    private void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            canUse = true;
        }

    }
}
