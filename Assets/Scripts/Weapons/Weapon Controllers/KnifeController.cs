using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeController : WeaponController //Así accede a nuestra función de armas, donde están todas las armas y sus características
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }
    protected override void Attack()
    {
        base.Attack();
        GameObject spawnedKnife = Instantiate(weaponData.Prefab);
        spawnedKnife.transform.position = transform.position; //Usa la ubicación de el gameobject asignado, que es el player
        spawnedKnife.GetComponent<KnifeBehavior>().DirectionChecker(pm.lastMovedVector); //Aquí ya se tomó la dirección del player
    }
}