using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingsPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentMoveSpeed *= 1 + passiveItemData.Multiplier / 100f;
        GameManager.instance.currentMoveSpeedDisplay.text = "Velocidad de movimiento: " + player.CurrentMoveSpeed.ToString("F2");
    }
}
