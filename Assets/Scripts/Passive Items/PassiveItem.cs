using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PassiveItem : MonoBehaviour
{
    protected PlayerStats player;
    public PassiveItemScriptableObject passiveItemData;

    protected virtual void ApplyModifier()
    {
        //Esta función aplicará los cambios en las stats

    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>();
        ApplyModifier();
    }
    //Guardalo
}
