using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPotion : Pickup
{
    public int healthToRestore;
    public AudioClip pickupSound;

    public override void Collect()
    {
        if(hasBeencollected)
        {
            return;
        }
        else
        {
            base.Collect();
        }
        SoundController.Instance.PlaySound(pickupSound);
        PlayerStats player = FindObjectOfType<PlayerStats>();
        player.RestoreHealth(healthToRestore);
    }

}
