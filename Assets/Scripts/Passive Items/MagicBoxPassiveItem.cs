using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicBoxPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentHealth *= 1 + passiveItemData.Multiplier / 100f;
        GameManager.instance.currentHealthDisplay.text = "Vida: " + player.CurrentHealth.ToString("F2");
    }
}
