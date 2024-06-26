using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GorkSpawnBullets : MonoBehaviour
{
    enum SpawnerType {Straight, Spin}

    [Header("Bullet Attributes")]
    public GameObject bullet;
    public float bulletlife = 1f;
    public float speed = 1f;
    public AudioClip bulletSound;

    [Header("Spawner Attributes")]
    [SerializeField] private SpawnerType spawnerType;
    [SerializeField] private float firingRate = 1f;

    private GameObject spawnedBullet;
    private float timer = 0f;
    void Update()
    {
        timer += Time.deltaTime;
        if(spawnerType == SpawnerType.Spin) transform.eulerAngles = new Vector3(0f, 0f, transform.eulerAngles.z+1f);
        if (timer >= firingRate)
        {
            Fire();
            timer = 0;
        }    
    }

    private void Fire()
    {
        if(bullet)
        {
            SoundController.Instance.PlaySound(bulletSound);
            spawnedBullet = Instantiate(bullet, transform.position, Quaternion.identity);
            spawnedBullet.GetComponent<GorkBullet>().speed = speed;
            spawnedBullet.GetComponent<GorkBullet>().bulletlife = bulletlife;
            spawnedBullet.transform.rotation = transform.rotation;
        }
    }
}
