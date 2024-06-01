using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceGem : Pickup
{
    public int experienceGranted;
    public AudioClip pickupSound;

    public override void Collect() //Para poder usar el interfaz ICollect, debemos poner dentro de esta función: throw new System.NotImplementedException(); y ya lo podemos eliminat
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
        player.IncreaseExperience(experienceGranted);
    }
}
