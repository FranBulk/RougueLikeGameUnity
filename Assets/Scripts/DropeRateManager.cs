using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropeRateManager : MonoBehaviour
{
    [System.Serializable]
    public class Drops
    {
        public string name;
        public GameObject itemPrefab;
        public float dropRate;
    }
    public List<Drops> drops;

    void OnDestroy() 
    {
        if(!gameObject.scene.isLoaded) //Esto quita ese error de spawnear cosas raro
        {
            return;
        }

        float randomNumber = UnityEngine.Random.Range(0f, 100f);
        List<Drops>possibleDrops = new List<Drops>();

        foreach (Drops rate in drops)
        {
            if(randomNumber <= rate.dropRate)
            {
                possibleDrops.Add(rate);
            }
        }
        //Si hay posibles drops los hace, y no los repite
        if (possibleDrops.Count > 0)
        {
            Drops drops = possibleDrops[UnityEngine.Random.Range(0, possibleDrops.Count)];
            Instantiate(drops.itemPrefab, transform.position, Quaternion.identity);   
        }
    }
}
