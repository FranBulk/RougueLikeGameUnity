using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorkMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player; //Es el jugador
    float movementx;
    public float CooldownGork;
    float maxCooldown;
    public GameObject bulletSpawner;
    bool Gorkispaused = false;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        CooldownGork = maxCooldown;
        CooldownGork = 0f;
        bulletSpawner.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        CooldownGork += Time.deltaTime;
        if(CooldownGork >= 2)
        {
            Gorkispaused = false;
        }
        if (CooldownGork == maxCooldown)
        {
            SpawnBulletSpawner();
            Gorkispaused = true;
            CooldownGork = 0f;
        }
        else if (!Gorkispaused)
        {
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, enemy.currentMovespeed * Time.deltaTime); //Es un movimiento constante hacia el jugador, accediendo con el tiempo real
            Vector2 direction = player.position - transform.position;
            if (direction.x > 0 && transform.localScale.x < 0)
            {
                Flip();
            }
            else if (direction.x < 0 && transform.localScale.x > 0)
            {
                Flip();
            }
        }
    }

    void Flip()
    {
        Vector3 scale = transform.localScale;
        scale.x *= -1; // Invertir la escala en el eje X para cambiar la orientación del sprite
        transform.localScale = scale;
    }

    void SpawnBulletSpawner()
    {
        bulletSpawner.SetActive(true);
    }
}