using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotenciadorPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentProjectileSpeed *= 1 + passiveItemData.Multiplier / 100f;
        GameManager.instance.currentProjectileSpeedDisplay.text = "Velocidad de Proyectil: " + player.CurrentProjectileSpeed.ToString("F2");
    }
}
