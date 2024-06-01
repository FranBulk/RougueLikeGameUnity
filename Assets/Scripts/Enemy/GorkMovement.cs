using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorkMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player; //Es el jugador
    float movementx;
    public float CooldownGork;
    public float timeUntilGorkShoot;
    float maxCooldown;
    public GameObject bulletSpawner;
    bool Gorkispaused = false;
    public AudioClip BulletSpawnSound;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
        maxCooldown = CooldownGork;
        CooldownGork = 0f;
        bulletSpawner.SetActive(false);
    }
    void Update()
    {
        CooldownGork += Time.deltaTime;
        if (!Gorkispaused)
        {
            if(CooldownGork >= timeUntilGorkShoot)
            {
                Gorkispaused = true;
            }
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
        else if (Gorkispaused)
        {
            SpawnBulletSpawner();
            if (CooldownGork >= maxCooldown+timeUntilGorkShoot)
            {
                Gorkispaused = false;
                CooldownGork = 0;
                bulletSpawner.SetActive(false);
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