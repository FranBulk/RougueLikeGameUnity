using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GorkStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    //Las stats del momento del juego, las que varian
    [HideInInspector]
    public float currentMovespeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    PlayerStats playerStats;

    public float despawnDistance = 20f;
    public AudioClip hitmarket;
    public Image healthBar;
    Transform player;
    void Awake()
    {
        currentMovespeed = enemyData.MoveSpeed;
        currentHealth = enemyData.Maxhealth;
        currentDamage = enemyData.Damage;
    }

    void Update()
    {
        if(Vector2.Distance(transform.position, player.position) >= despawnDistance)
        {
            ReturnEnemy();
        }
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        UpdateHealthBar();
    }
    public void TakeDamage(float dmg)
    {
        SoundController.Instance.PlaySound(hitmarket);
        currentHealth -= dmg;
        if (currentHealth <=0)
        {
            KillGork();
        }

        UpdateHealthBar();
    }
    public void KillGork()
    {
        playerStats.Kill();
        GameManager.instance.GameOver();
        Destroy(gameObject);
    }

    private void OnCollisionStay2D(Collision2D col) 
    {
        //Comparando con el personaje haciendo tags, y dañando con la función tomar daño
        if(col.gameObject.CompareTag("Player"))
        {
            PlayerStats player = col.gameObject.GetComponent<PlayerStats>();
            player.TakeDamage(currentDamage);
        }    
    }

    private void OnDestroy() 
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        es.OnEnemyKilled();
    }

    void ReturnEnemy()
    {
        EnemySpawner es = FindObjectOfType<EnemySpawner>();
        transform.position = player.position + es.relativeSpawnpoints[Random.Range(0, es.relativeSpawnpoints.Count)].position;
    }

    void UpdateHealthBar()
    {
        healthBar.fillAmount = currentHealth / enemyData.Maxhealth;
    }
}
