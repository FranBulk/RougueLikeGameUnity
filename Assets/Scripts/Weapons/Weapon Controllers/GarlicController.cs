using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GarlicController : WeaponController
{
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedGarlic = Instantiate(weaponData.Prefab); //Este va a ser el prefab que se espawnee
        spawnedGarlic.transform.position = transform.position; //Asignamos la posición de nuestro prefab a la del jugador
        spawnedGarlic.transform.parent = transform; //Spawnea en el jugador
    }
}
