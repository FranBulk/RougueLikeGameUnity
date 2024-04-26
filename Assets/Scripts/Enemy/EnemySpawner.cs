using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    public class Wave //Oleadas de enemigos
    {
        public string waveName; //El nombre de cada ola
        public List<EnemyGroup> enemyGroups; //La lista de los grupos de enemigos que vamos a spawnear
        public int waveQuota; //La cantidad de enemigos total que spawnean
        public float spawnInterval; //El tiempo entre cada enemigo
        public int spawnCount; //La cantidad de enemigos que ya spawnearon
    }

    [System.Serializable]
    public class EnemyGroup //Esta clase es para cada enemigo en solitario
    {
        public string enemyName; //El nombre del enemigo
        public int enemyCount; //Cuantos enemigos hay para spawnear
        public int spawnCount; //Cuantos enemigos hay spawneados
        public GameObject enemyPrefab; //El prefab de el enemigo
    }

    public List<Wave> waves; //Una lista de todas las oleadas de enemigos en el juego
    public int currentWaveCount; //Es la oleada que está en curso, Empieza en 0 ya que es una lista

    [Header("Spawner Attributes")]
    float spawnTimer; //El cooldown entre cada spawn
    public int enemiesAlive; //El numero de enemigos vivos en la escena
    public int maxEnemiesAllowed; //Numero máximo de enemigos permitidos
    public bool maxEnemiesReached = false; //Si ya llegó al numero máximo de enemigos
    public float waveInterval; //El intervalo entre cada oleada

    [Header("Spawn Positions")]
    public List<Transform> relativeSpawnpoints; //Una lista para ver todos los spawn points de nuestro mapa

    Transform player;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        CalculateWaveQuota();      
    }

    void Update()
    {

        if(currentWaveCount < waves.Count && waves[currentWaveCount].spawnCount == 0) //Checa si la oleada ya terminó
        {
            StartCoroutine(BeginNextWave());
        }
        spawnTimer += Time.deltaTime;

        //Si se acaba el cooldown, spawnea otra vez
        if(spawnTimer >= waves[currentWaveCount].spawnInterval)
        {
            spawnTimer = 0f;
            SpawnEnemies();
        }
    }

    IEnumerator BeginNextWave()
    {
        yield return new WaitForSeconds(waveInterval);

        if(currentWaveCount < waves.Count-1)
        {
            currentWaveCount++;
            CalculateWaveQuota();
        }
    }

    void CalculateWaveQuota() //Esta función calculará la oledad que esté en curso
    {
        int currentWaveQuota = 0;
        foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
        {
            currentWaveQuota += enemyGroup.enemyCount;
        }

        waves[currentWaveCount].waveQuota = currentWaveQuota;
        Debug.LogWarning(currentWaveQuota);
    }

    void SpawnEnemies()
    {
        //Valida si la mínima cantidad de enemigos ya se spawneó
        if(waves[currentWaveCount].spawnCount < waves[currentWaveCount].waveQuota && !maxEnemiesReached)
        {
            //Spawnea cada enemigo en la oleda transcurrente
            foreach (var enemyGroup in waves[currentWaveCount].enemyGroups)
            {
                //Valida si ya se spawneó la mínima cantidad de enemigos de cada tipo
                if(enemyGroup.spawnCount < enemyGroup.enemyCount)
                {
                    //Así limitamos el número máximo
                    if(enemiesAlive >= maxEnemiesAllowed)
                    {
                        maxEnemiesReached = true;
                        return;
                    }
                    //Spawnear los enemigos en los spawnpoints del mapa, de manera aleatoria
                    Instantiate(enemyGroup.enemyPrefab, player.position + relativeSpawnpoints[Random.Range(0, relativeSpawnpoints.Count)].position, Quaternion.identity);

                    enemyGroup.spawnCount++;
                    waves[currentWaveCount].spawnCount++;
                    enemiesAlive++;
                }
            }
        }
        //Reseteamos la variable de la cantidad máxima de enemigos
        if(enemiesAlive < maxEnemiesAllowed)
        {
            maxEnemiesReached = false;
        }
    }

    public void OnEnemyKilled()
    {
        //Si un enemigo fué asesinado
        enemiesAlive--;
    }
}
