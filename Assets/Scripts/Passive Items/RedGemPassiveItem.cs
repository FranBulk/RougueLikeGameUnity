using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedGemPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentRecovery *= 2 + passiveItemData.Multiplier / 100f;
        GameManager.instance.currentrecoveryDisplay.text = "Recuperación: " + player.CurrentRecovery.ToString("F2");
    }
}
