using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetStrike : Abilities
{
    public GameObject VFX;
    public GameObject planet;
    private Transform player;


    private void Update()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }
    public override void GetUse()
    {
        if (boss.mana > manaConsumed)
        {
            anim.SetTrigger("casting");
            Instantiate(VFX, transform.position, transform.rotation);
            boss.mana -= manaConsumed;
        }
    }

    public void SpawnPlanet()
    {
        var whereToSpawn = new Vector3(player.position.x, player.position.y + 20, player.position.z);
        var rotation = Quaternion.Euler(90, 0, 0);
        Instantiate(planet, whereToSpawn, rotation);
    }
}
