using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotenciadorPassiveItem : PassiveItem
{
    protected override void ApplyModifier()
    {
        player.CurrentProjectileSpeed *= 1 + passiveItemData.Multiplier / 100f;
    }
}
