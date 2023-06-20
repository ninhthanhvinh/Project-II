using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldGenerate : Abilities
{
    public GameObject shield;

    protected float timer;
    protected bool canUse;
    public override void GetUse()
    {
        if (boss.mana >= manaConsumed && canUse)
        {
            shield.SetActive(true);
            boss.mana -= manaConsumed;
            timer = CD;
            canUse = false;
        }
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
