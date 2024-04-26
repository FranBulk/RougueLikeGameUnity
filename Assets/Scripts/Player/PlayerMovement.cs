using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Movimiento
    [HideInInspector]//Para que no se le pueda mover desde Unity
    public float lastHorizontalVector; //Para hacer que el personaje se quede en la última posición
    [HideInInspector]//Para que no se le pueda mover desde Unity
    public float lastVerticalVector; //Para hacer que el personaje se quede en la última posición
    [HideInInspector]//Para que no se le pueda mover desde Unity
    public Vector2 moveDir; //Es el vector de movimiento del personaje
    [HideInInspector]
    public Vector2 lastMovedVector; //Es el último movimiento del personaje

    //Referencias
    Rigidbody2D rb; //Es el componente para encontrar a nuestro personaje
    PlayerStats player;

    void Start()
    {
        player = GetComponent<PlayerStats>();
        rb=GetComponent<Rigidbody2D>(); //Obtiene el rigidbody de el personaje, y lo guarda en nuestra variable
        lastMovedVector = new Vector2(1, 0f); //Si no se mueve
    }

    void Update()
    {
        InputManagement(); //Llamamos a nuestra función que obtiene los 2 componentes
    }

    void FixedUpdate() //Es mas útil para funciones de movimiento
    {
        Move(); //Llamamos a nuestra función de movimiento
    }

    void InputManagement() //Donde va a recibir la entrada del usuario
    {
        if(GameManager.instance.isGameOver)
        {
            return;
        }
        float moveX = Input.GetAxisRaw("Horizontal"); //Donde va a guardar la entrada para derecha e izquierda
        float moveY = Input.GetAxisRaw("Vertical"); //Donde va a guardar la entrada para arriba y abajo
        moveDir = new Vector2(moveX, moveY).normalized; //Aquí se forma el nuevo vector con las 2 entradas
        if (moveDir.x != 0)
        {
            lastHorizontalVector = moveDir.x; //Para que se quede guardado la orientación
            lastMovedVector = new Vector2(lastHorizontalVector, 0f); //Última vez que se movió en x
        }
        if (moveDir.y != 0)
        {
            lastVerticalVector = moveDir.y; //Para que se quede guardado la orientación
            lastMovedVector = new Vector2(0f, lastVerticalVector); //Última vez que se movió en y
        }
        if(moveDir.x != 0 && moveDir.y != 0)
        {
            lastMovedVector = new Vector2(lastHorizontalVector, lastVerticalVector); //Mientras hay movimiento
        }
    }

    void Move() //Administrar el movimiento del player
    {
         if(GameManager.instance.isGameOver)
        {
            return;
        }
        rb.velocity = new Vector2 (moveDir.x * player.CurrentMoveSpeed, moveDir.y * player.CurrentMoveSpeed);//Para la velocidad del movimiento, se multiplican las entradas por la velocidad
    }
}
