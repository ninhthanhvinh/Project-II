using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Abilities : MonoBehaviour
{
    protected Boss boss;
    protected Animator anim;
    public float manaConsumed;
    public float CD;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        boss = GetComponent<Boss>();
    }
    public abstract void GetUse();
}
