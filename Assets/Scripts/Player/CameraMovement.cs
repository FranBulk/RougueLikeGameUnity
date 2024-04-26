using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    public Transform target; //Es a lo que va a seguir
    public Vector3 offset; //Es el movimiento de la camara

    void Update()
    {
        transform.position = target.position + offset;
    }
}
