using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterScriptableObject", menuName = "ScriptableObjects/Character")]

public class CharacterScriptableObject : ScriptableObject
{
    [SerializeField]
    Sprite icon;
    public Sprite Icon {get => icon; private set => icon = value;}
    [SerializeField]
    new string name;
    public string Name {get => name; private set => name = value;}
    [SerializeField]
    GameObject startingWeapon; //El arma inicial del personaje que vamos a hacer
    public GameObject Startingweapon {get => startingWeapon; private set => startingWeapon = value;}
    [SerializeField]
    float maxHealth; //Su salud máxima del personaje
    public float MaxHealth {get => maxHealth; private set => maxHealth = value;}
    [SerializeField]
    float recovery; //La regeneración del personaje
    public float Recovery {get => recovery; private set => recovery = value;}
    [SerializeField]
    float moveSpeed;
    public float MoveSpeed {get => moveSpeed; private set => moveSpeed = value;}
    [SerializeField]
    float might;
    public float Might {get => might; private set => might = value;}
    [SerializeField]
    float projectileSpeed;
    public float ProjectileSpeed {get => projectileSpeed; private set => projectileSpeed = value;}
    [SerializeField]
    float magnet;
    public float Magnet {get => magnet; private set => magnet = value;}
}
