using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Este es el script para todas las armas

public class WeaponController : MonoBehaviour
{
    [Header ("Weapon Stats")]
    public WeaponScriptableObject weaponData; //Para relacionar nuestras estats de las armas y sus características
    float currentCooldown; //El cooldown actual de nuestra arma
    int timesrepeat;

    protected PlayerMovement pm;
    
    // Start is called before the first frame update
    protected virtual void Start() //Virtual es para que cada hijo pueda acceder a las funciones de papa, como esto es la función principal, las armas accederán a ellas
    {
        pm = FindObjectOfType<PlayerMovement>();
        currentCooldown = weaponData.CooldownDuration; //En cuanto el juego empieza, el arma se usa, entonces empieza el cooldown
        timesrepeat = weaponData.Repeat;
    }

    // Update is called once per frame
    protected virtual void Update()//Virtual es para que cada hijo pueda acceder a las funciones de papa, como esto es la función principal, las armas accederán a ellas
    {
        currentCooldown -= Time.deltaTime;
        if(currentCooldown <= 0f) //Cuando el cooldown es igual a cero o menor, quiere decir que ya no hay, por lo que ataca
        {
            for (int i = 0; i < timesrepeat; i++)
            {
                if(i == 0)
                {
                    Invoke("Attack", 0f);
                }
                else
                {
                    Invoke("Attack", weaponData.CooldownBetween);
                }
            }
        }
    }
    protected virtual void Attack()//Virtual es para que cada hijo pueda acceder a las funciones de papa, como esto es la función principal, las armas accederán a ellas
    {
        currentCooldown = weaponData.CooldownDuration; //Ya que atacó, se reinica el cooldown y así entramos en el loop
    }
}
//Al rato lo cambio