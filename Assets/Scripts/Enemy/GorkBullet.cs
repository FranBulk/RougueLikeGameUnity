using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GorkBullet : MonoBehaviour
{
    public float bulletlife = 1f;
    public float rotation = 0f;
    public float speed = 1f;

    private Vector2 spawnPoint;
    private float timer = 0f;
    Transform player;

    void Start()
    {
        spawnPoint = new Vector2(transform.position.x, transform.position.y);
        player = FindObjectOfType<PlayerStats>().transform;   
    }

    void Update()
    {
        if (timer > bulletlife) Destroy(this.gameObject);
        timer += Time.deltaTime;
        transform.position = Movement(timer);
    }

    private Vector2 Movement(float timer)
    {
        float x = timer * speed * transform.right.x;
        float y = timer * speed * transform.right.y;
        return new Vector2(x+spawnPoint.x, y+spawnPoint.y);
    }
    private void OnCollisionStay2D(Collision2D col) 
    {
        //Comparando con el personaje haciendo tags, y dañando con la función tomar daño
        if(col.gameObject.CompareTag("Player"))
        {
            Destroy(this.gameObject);
        }    
    }
}
