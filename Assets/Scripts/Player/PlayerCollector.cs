using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollector : MonoBehaviour
{
    PlayerStats player;
    CircleCollider2D playerCollector;
    public float pullSpeed;

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        playerCollector = GetComponent<CircleCollider2D>();
    }

    void Update()
    {
        playerCollector.radius = player.CurrentMagnet;
    }


    void OnTriggerEnter2D(Collider2D col) 
    {
        //Con esto validas que el objeto que está en contacto, tenga la interfaz de ICollectible
        if(col.gameObject.TryGetComponent(out ICollectible collectible))
        {
            //Animación de jalar para los objetos
            Rigidbody2D rb = col.gameObject.GetComponent<Rigidbody2D>();
            Vector2 forceDirection = (transform.position - col.transform.position).normalized;
            rb.AddForce(forceDirection * pullSpeed);

            //Si lo hace, llama a la función collect
            collectible.Collect();
        }
    }
}
