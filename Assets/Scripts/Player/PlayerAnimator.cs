using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{
    // Referencias
    Animator am; //Es la referencia a nuestro animador
    PlayerMovement pm; //Es la referencia a nuestro script
    SpriteRenderer sr; //Es la referencia a nuestro sprites

    void Start()
    {
        am = GetComponent<Animator>(); //am toma el animator que le asignamos al objeto
        pm = GetComponent<PlayerMovement>(); //pm toma el script de movimiento del objeto
        sr = GetComponent<SpriteRenderer>();//sr toma el sprite del objeto
    }

    // Update is called once per frame
    void Update()
    {
        if (pm.moveDir.x != 0 || pm.moveDir.y != 0) //Si cualquiera de las 2 condiciones se cumple, es que se está moviendo
        {
            am.SetBool("Move",true); //Quiere decir que el momento de am se va a mover
            SpriteDirectionChecker();
        }
        else
        {
            am.SetBool("Move",false); //Quiere decir que no se va mover
        }
    }

    void SpriteDirectionChecker() //Para saber la direccion y rotar la animación
    {
        if(pm.lastHorizontalVector < 0) //Si la condición se cumple, el usuario está presionando la tecla izquierda
        {
            sr.flipX = true; //Se voltea 90 grados el script
        }
        else
        {
            sr.flipX = false; //No se hace nada
        }
    }
}
