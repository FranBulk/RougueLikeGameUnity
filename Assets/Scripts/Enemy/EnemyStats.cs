using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;
    //Las stats del momento del juego, las que varian
    [HideInInspector]
    public float currentMovespeed;
    [HideInInspector]
    public float currentHealth;
    [HideInInspector]
    public float currentDamage;

    public float despawnDistance = 20f;
    public AudioClip hitmarket;
    Transform player;
    public Image healthBar;
    public bool IsGork;
    PlayerStats playerStats;
    InventoryManager inventory;
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
    Debug.Log("Damage taken: " + dmg);
    Debug.Log("Current health: " + currentHealth);

    if (currentHealth <= 0)
    {
        HandleDeath();
    }
    else
    {
        UpdateHealthBar();
    }
}

private void HandleDeath()
{
    if (IsGork)
    {
        Debug.Log("Is Gork");

        if (!GameManager.instance.isGameOver)
        {
            Debug.Log("Game is not over yet");

            Kill();
            GameManager.instance.AssignLevelReachedUI(playerStats.level);
            GameManager.instance.AssignChosenWeaponsAndPassiveItemsUI(inventory.weaponUISlots, inventory.passiveItemUISlots);
            GameManager.instance.GameOver();
            Debug.Log("GameOver called");
        }
        else
        {
            Debug.Log("Game is already over");
        }
    }
    else
    {
        Debug.Log("Is not Gork");
        Kill();
    }
}
    public void Kill()
    {
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
