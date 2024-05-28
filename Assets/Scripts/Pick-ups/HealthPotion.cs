using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup, ICollectible
{
    public int healthToRestore;
    public AudioClip pickupSound;

    public void Collect()
    {
        SoundController.Instance.PlaySound(pickupSound);
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

}
