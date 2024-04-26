using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropRandomaizer : MonoBehaviour
{
    public List<GameObject> propSpawnPoints; //Es la lista de nuestros puntos del chunk
    public List<GameObject> propPrefabs; //Es la lista de nuestros prefabs de escena
    void Start()
    {
        SpawnProps();
    }

    void Update()
    {
        
    }

    void SpawnProps()
    {
        foreach (GameObject sp in propSpawnPoints)
        {
            int rand = Random.Range(0, propPrefabs.Count);
            GameObject prop = Instantiate(propPrefabs[rand], sp.transform.position, Quaternion.identity);
            prop.transform.parent = sp.transform;
        }
    }
}
