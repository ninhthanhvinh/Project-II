using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageable
{
    public abstract void GetDamage(float dmg);
    public abstract void GetHeal(float healAmount);
}
