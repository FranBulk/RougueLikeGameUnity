using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnifeBehavior : ProjectileWeaponBehavior //Que es un hijo de nuestra funcion de los proyectiles
{
    //Guardalo
    protected override void Start()
    {
        base.Start();
    }
    void Update()
    {
        transform.position += direction * currentSpeed * Time.deltaTime; //Es el movimiento del cuchillo 
    }
}