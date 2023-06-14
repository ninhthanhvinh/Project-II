using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerfulSlice : Abilities
{
    [SerializeField] Transform whereToSpawnVFX;
    [SerializeField] GameObject sliceHit;


    public override void GetUse()
    {
        if (boss.mana >= manaConsumed)
        {
           anim.SetTrigger("slice01");
           boss.mana -= manaConsumed;
        }
    }

    public void Slash()
    {
        Instantiate(sliceHit, whereToSpawnVFX.position, transform.rotation);
    }
}
