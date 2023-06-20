using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalAttack : Abilities
{

    protected float timer;
    protected bool canUse;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public override void GetUse()
    {
        anim.Play("Attack1");
    }
}
