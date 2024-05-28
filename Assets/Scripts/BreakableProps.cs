using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BreakableProps : MonoBehaviour
{
    public float health;
    public AudioClip breakSound;
    public AudioClip hitMarket;
    public void TakeDamage(float dmg)
    {
        SoundController.Instance.PlaySound(hitMarket);
        health -= dmg;
        
        if(health <= 0)
        {
            SoundController.Instance.PlaySound(breakSound);
            Kill();
        }
    }

    public void Kill()
    {
        Destroy(gameObject);
    }
}
